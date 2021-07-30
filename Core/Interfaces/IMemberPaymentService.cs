using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberPaymentService
    {
        Task<MemberPayment> CreatePaymentAsync(MemberPayment payment);
        Task<IReadOnlyList<MemberPayment>> GetPaymentsAsync();
        Task<MemberPayment> GetPaymentAsync(string paymentId);
        Task<IReadOnlyList<MemberPayment>> GetMemberPaymentsAsync(string memberId);
        Task<MemberPayment> UpdatePaymentAsync(MemberPayment payment);
        Task<bool> DeletePaymentAsync(string paymentId);
    }
}
