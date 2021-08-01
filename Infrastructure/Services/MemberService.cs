using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
using DataBase.UnitOfWork;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        //allow sentence with only one space
        Regex regex = new Regex("[ ]{2,}", RegexOptions.None);

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Member> CreateMemberAsync(
            Member memberToCreate,
            int? type,
            int? method,
            decimal total,
            double? tax,
            double? discount,
            string note,
            decimal scheduledpaymenamount,
            int scheduledevery)
        {
            if (total == 0 || type == null || !Enum.IsDefined(typeof(PaymentType), type) || ((type == ((int)PaymentType.Scheduled)) && (scheduledpaymenamount == default || scheduledevery == 0)))
                return null;

            //Replace multiple White spaces between words to one space
            memberToCreate.Name = regex.Replace(memberToCreate.Name, " ").Trim();
            //Remove WhiteSpaces
            memberToCreate.Code = regex.Replace(memberToCreate.Code, " ").Replace(" ","").Trim();
            memberToCreate.RelationShip = regex.Replace(memberToCreate.RelationShip, " ").Replace(" ","").Trim();
            //Check for exact name dublicate
            var DublicateName = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Name.ToLower() == memberToCreate.Name.ToLower(), track: false);
            if (DublicateName != null) return null;
            // ensure every code group has one owner
            var DublicateOwner = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Code == memberToCreate.Code && x.IsOwner == true, track: false);
            //if the created member is owner and the code group already has owner return null
            if (memberToCreate.IsOwner && DublicateOwner is not null ) return null;
            //if the created member is not owner and the code group has no owner return null
            if (!memberToCreate.IsOwner && DublicateOwner is null) return null;
            await _unitOfWork.Repository<Member>().AddItemAsync(memberToCreate);
            var memberHistory = new MemberHistory {MemberId = memberToCreate.Id, Date = DateTimeOffset.Now, Title = "Added" };
            await _unitOfWork.Repository<MemberHistory>().AddItemAsync(memberHistory);

            var memberPayment = new MemberPayment
            {
                Date = DateTimeOffset.Now,
                DiscountPercentage = discount,
                Name = "Addition",
                Note = note,
                PaymentTotal = total,
                PaymentType = (PaymentType)type,
                TaxPercentage = tax,
                PaymentMethod = type == ((int)PaymentType.Scheduled) ? null : (PaymentMethod)method,
                MemberId = memberToCreate.Id

            };
            await _unitOfWork.Repository<MemberPayment>().AddItemAsync(memberPayment);

            if (type == ((int)PaymentType.Scheduled))
            {
                var scheduledPayments = new List<ScheduledPayment>();

                var scheduledNumber = Math.Ceiling(total / scheduledpaymenamount);
                scheduledPayments.Add(new ScheduledPayment
                {
                    MemberPaymentId = memberPayment.Id,
                    PaymentAmount = scheduledpaymenamount,
                    PaymentMethod = (PaymentMethod)method,
                    Fulfiled = true,
                    PaymentDueDate = DateTimeOffset.Now,
                    FulfiledDate = DateTimeOffset.Now

                });

                for (int i = 1; i < scheduledNumber; i++)
                {
                    var lastPaymentDate = scheduledPayments[i - 1].PaymentDueDate;
                    scheduledPayments.Add(new ScheduledPayment
                    {
                        MemberPaymentId = memberPayment.Id,
                        PaymentAmount = scheduledpaymenamount,
                        PaymentDueDate = lastPaymentDate.AddDays(scheduledevery)

                    });
                }

                foreach (var item in scheduledPayments)
                {
                    await _unitOfWork.Repository<ScheduledPayment>().AddItemAsync(item);

                }
            }

            // save to db
            if (await _unitOfWork.Complete()) return memberToCreate;
            return null;
        }
        
        public async Task<bool> DeleteMemberAsync(string memberId)
        {
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == memberId);
            if (member == null) return false;
            await _unitOfWork.Repository<Member>().DeleteItemAsync(member);
            if (await _unitOfWork.Complete()) return true;

            return false;

        }

        public async Task<Member> GetMemberAsync(string memberId)
        {
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == memberId, $"{nameof(Member.MemberDetails)}", false);

            if (member == null) return null;

            return member;
        }

        public async Task<IReadOnlyList<Member>> GetMembersAsync(
            string memberName = null,
            string code = null,
            bool? isowner = null,
            int? status = null,
            string relationshop = null,
            string note = null)
        {
            Expression<Func<Member, bool>> predicateExpression = null;

            // split search by name to multiple word
            var keywords = memberName?.Split(' ').Select(s => s.ToLower()).ToArray();

            // The lambda parameter.
            var memberParameter = Expression.Parameter(typeof(Member), "i");

            // Build the individual conditions to check against.
            var orConditions = keywords?
                .Select(keyword => (Expression<Func<Member, bool>>)(i => (i.Name.ToLower().Contains(keyword)) &&
                (!string.IsNullOrWhiteSpace(relationshop) ? i.RelationShip.ToLower().Contains(relationshop.ToLower()) : true) &&
                (!string.IsNullOrWhiteSpace(note) ? i.Note.ToLower().Contains(note.ToLower()) : true) &&
                (!string.IsNullOrWhiteSpace(code) ? i.Code.ToLower().Contains(code.ToLower()) : true) &&
                (isowner != null ? i.IsOwner == isowner : true) &&
                ((status != null && Enum.IsDefined(typeof(MemberStatus), status)) ? i.MemberStatus == (MemberStatus)status : true)
                ))
                .Select(lambda => (Expression)Expression.Invoke(lambda, memberParameter));

            // Combine the individual conditions to an expression tree of nested ORs.
            var orExpressionTree = orConditions?
                .Skip(1)
                .Aggregate(
                    orConditions.First(),
                    (current, expression) => Expression.AndAlso(expression, current));

            if (!string.IsNullOrWhiteSpace(memberName))
            {
                // Build the final predicate (a lambda expression), so we can use it inside of `.Where()`.
                predicateExpression = (Expression<Func<Member, bool>>)Expression.Lambda(
                    orExpressionTree,
                    memberParameter);

                return await _unitOfWork.Repository<Member>().Get(predicateExpression, orderBy: x => x.OrderBy(y => y.Name), track: false);
            }

            return await _unitOfWork.Repository<Member>().Get(x =>
                (!string.IsNullOrWhiteSpace(relationshop) ? x.RelationShip.ToLower().Contains(relationshop.ToLower()) : true) &&
                (!string.IsNullOrWhiteSpace(code) ? x.Code.ToLower().Contains(code.ToLower()) : true) &&
                (!string.IsNullOrWhiteSpace(note) ? x.Note.ToLower().Contains(note.ToLower()) : true) &&
                (isowner != null ? x.IsOwner == isowner : true) &&
                ((status != null && Enum.IsDefined(typeof(MemberStatus), status)) ? x.MemberStatus == (MemberStatus)status : true),
                orderBy: x => x.OrderBy(y => y.Name), track: false);


            //if (string.IsNullOrWhiteSpace(memberName) && !string.IsNullOrWhiteSpace(code))
            //    return await _unitOfWork.Repository<Member>().Get(x => x.Code.ToLower().Contains(code.ToLower()));
            //else if(!string.IsNullOrWhiteSpace(memberName) && string.IsNullOrWhiteSpace(code))

            //    //return await _unitOfWork.Repository<Member>().Get(x => keywords.Any(val => x.Name.Contains(val)));
            ////return await _unitOfWork.Repository<Member>().Get(x => x.Name.ToLower().Contains(memberName.ToLower()));
            //else if(!string.IsNullOrWhiteSpace(memberName) && !string.IsNullOrWhiteSpace(code))
            //    return await _unitOfWork.Repository<Member>().Get(x => x.Code.ToLower().Contains(code.ToLower()) && x.Name.ToLower().Contains(memberName.ToLower()));

            //return await _unitOfWork.Repository<Member>().GetAllAsync();

        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            await _unitOfWork.Repository<Member>().UpdateItemAsync(member);
            // save to db
            if (await _unitOfWork.Complete()) return member;

            return null;
        }
    }
}
