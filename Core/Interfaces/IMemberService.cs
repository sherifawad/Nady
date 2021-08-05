using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberService
    {
        Task<IReadOnlyList<Member>> GetMembersAsync(
            string memberName = null,
            string code = null,
            bool? isowner = null,
            int? status = null,
            string relationshop = null,
            string note = null);
        Task<Member> GetMemberAsync(string memberId);
        Task<Member> UpdateMemberAsync(Member member,
            string userId);
        Task<bool> DeleteMemberAsync(string memberId,
            string userId);
        Task<Member> CreateMemberAsync(
            Member memberToCreate,
            int? type,
            int? method,
            decimal total,
            double? tax,
            double? discount,
            string note,
            decimal scheduledpaymenamount,
            int scheduledevery,
            string userId);
        Task<Member> GetLastMember();
    }
}
