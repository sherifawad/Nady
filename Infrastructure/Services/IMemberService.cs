﻿using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IMemberService
    {
        Task<Member> CreateMemberAsync(string name, bool isOwner = false, string Code = null, string relationship = null);
        Task<IReadOnlyList<Member>> GetMembersAsync();
        Task<Member> GetMemberAsync(string memberId);
        Task<IReadOnlyList<MemberPayment>> GetMemberPaymentAsync(string memberId);
        Task<IReadOnlyList<MemberHistory>> GetMemberHistoriesAsync(string memberId);
        Task<IReadOnlyList<MemberVisitor>> GetMemberVisitorsAsync(string memberId);
        Task<IReadOnlyList<Member>> GetMembersByNameAsync(string memberName);
        Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(string PaymentId);
        Task<Member> UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(string memberId);
    }
}