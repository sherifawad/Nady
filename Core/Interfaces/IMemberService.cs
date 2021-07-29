using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberService
    {
        Task<Member> CreateMemberAsync(Member member);
        Task<IReadOnlyList<Member>> GetMembersAsync(string memberName = null, string code = null);
        Task<Member> GetMemberAsync(string memberId);
        Task<IReadOnlyList<MemberPayment>> GetMemberPaymentAsync(string memberId);
        Task<IReadOnlyList<MemberHistory>> GetMemberHistoriesAsync(string memberId);
        Task<IReadOnlyList<MemberVisitor>> GetMemberVisitorsAsync(string memberId);
        Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(string PaymentId);
        Task<Member> UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(string memberId);
    }
}
