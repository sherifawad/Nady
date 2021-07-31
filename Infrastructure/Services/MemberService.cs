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
        public async Task<Member> CreateMemberAsync(Member memberToCreate, bool isScheduled, int type, int? method, decimal amount, decimal total, double tax, double discount, DateTimeOffset date, string note, decimal scheduledpaymenamount, int scheduledevery)
        {
            var DublicateName = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Name.ToLower().Replace(" ", "") == memberToCreate.Name.ToLower().Replace(" ", ""), track: false);
            if (DublicateName != null) return null;
            //member.MemberHistoriesList.Add(memberHistory);
            //member.MemberPayments.Add(payment);
            memberToCreate.Name = regex.Replace(memberToCreate.Name, " ") .Trim();
            await _unitOfWork.Repository<Member>().AddItemAsync(memberToCreate);
            var memberHistory = new MemberHistory {MemberId = memberToCreate.Id, Date = DateTimeOffset.Now, Title = "Added" };
            await _unitOfWork.Repository<MemberHistory>().AddItemAsync(memberHistory);

            var memberPayment = new MemberPayment
            {
                Date = DateTimeOffset.Now,
                DiscountPercentage = discount,
                IsScheduled = isScheduled,
                Name = "Addition",
                Note = note,
                PaymentTotal = total,
                PaymentType = (PaymentType)type,
                TaxPercentage = tax,
                PaymentMethod = isScheduled? null : (PaymentMethod)method,
                MemberId = memberToCreate.Id

            };
            await _unitOfWork.Repository<MemberPayment>().AddItemAsync(memberPayment);

            if (isScheduled)
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

        public async Task<IReadOnlyList<Member>> GetMembersAsync(string memberName = null, string code = null)
        {
            Expression<Func<Member, bool>> predicateExpression = null;

            // split search by name to multiple word
            var keywords = memberName?.Split(' ').Select(s => s.ToLower()).ToArray();

            // The lambda parameter.
            var memberParameter = Expression.Parameter(typeof(Member), "i");

            // Build the individual conditions to check against.
            var orConditions = keywords?
                .Select(keyword => (Expression<Func<Member, bool>>)(i => i.Name.ToLower().Contains(keyword)))
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
            }


            if (string.IsNullOrWhiteSpace(memberName) && !string.IsNullOrWhiteSpace(code))
                return await _unitOfWork.Repository<Member>().Get(x => x.Code.ToLower().Contains(code.ToLower()));
            else if(!string.IsNullOrWhiteSpace(memberName) && string.IsNullOrWhiteSpace(code))
                return await _unitOfWork.Repository<Member>().Get(predicateExpression);
                //return await _unitOfWork.Repository<Member>().Get(x => keywords.Any(val => x.Name.Contains(val)));
            //return await _unitOfWork.Repository<Member>().Get(x => x.Name.ToLower().Contains(memberName.ToLower()));
            else if(!string.IsNullOrWhiteSpace(memberName) && !string.IsNullOrWhiteSpace(code))
                return await _unitOfWork.Repository<Member>().Get(x => x.Code.ToLower().Contains(code.ToLower()) && x.Name.ToLower().Contains(memberName.ToLower()));

            return await _unitOfWork.Repository<Member>().GetAllAsync();

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
