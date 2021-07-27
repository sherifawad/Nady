using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IScheduledPaymentService
    {
        Task<ScheduledPayment> CreateScheduledPaymentAsync(string memberPaymentId, string name, PaymentMethod paymentMethod, DateTime dueDate, decimal total, DateTime? fulfilledDate, bool fulfilled = false, string note = null);
        Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync();
        Task<ScheduledPayment> GetScheduledPaymentAsync(string scheduledPaymentId);
        Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(string paymentId);
        Task<ScheduledPayment> UpdatePaymentAsync(ScheduledPayment scheduledPayment);
        Task<bool> DeletePaymentAsync(string scheduledPaymentId);
    }
}
