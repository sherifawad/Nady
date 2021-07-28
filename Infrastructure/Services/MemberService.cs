using Core.Interfaces;
using Core.Models;
using DataBase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Member> CreateMemberAsync(string name, MemberDetails memberDetails, bool isOwner = false, string Code = null, string relationship = null)
        {
            var DublicateName = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (DublicateName != null) return null;
            memberDetails = memberDetails ?? new MemberDetails { NickName = "Mr." };
            var member = new Member { Name = name, IsOwner = isOwner, Code = Code, RelationShip = relationship, MemberDetails = memberDetails};
            member.MemberHistoriesList = new List<MemberHistory>();
            member.MemberHistoriesList.Add(new MemberHistory { Date = DateTime.Now, Title = "Added" });
            await _unitOfWork.Repository<Member>().AddItemAsync(member);
            // save to db
            if (await _unitOfWork.Complete()) return member;
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
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == memberId, $"{nameof(Member.MemberDetails)}");

            if (member == null) return null;

            return member;
        }

        public async Task<IReadOnlyList<MemberHistory>> GetMemberHistoriesAsync(string memberId)
        {
            var histories = await _unitOfWork.Repository<MemberHistory>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.Date));

            if (histories == null) return null;

            return histories;
        }

        public async Task<IReadOnlyList<MemberPayment>> GetMemberPaymentAsync(string memberId)
        {
            var Payments = await _unitOfWork.Repository<MemberPayment>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.Date));

            if (Payments == null) return null;

            return Payments;
        }

        public async Task<IReadOnlyList<Member>> GetMembersAsync(string memberName = null)
        {
            if(string.IsNullOrWhiteSpace(memberName))
                return await _unitOfWork.Repository<Member>().GetAllAsync();
            else
                return await _unitOfWork.Repository<Member>().Get(x => x.Name.ToLower().Contains(memberName.ToLower()));
        }

        public async Task<IReadOnlyList<MemberVisitor>> GetMemberVisitorsAsync(string memberId)
        {
            return await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.AccessesDate));
        }

        public async Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(string PaymentId)
        {
            return await _unitOfWork.Repository<ScheduledPayment>().Get(x => x.MemberPaymentId == PaymentId, orderBy: x => x.OrderBy(y => y.MemberPayment.Date));
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
