using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IScheduledPaymentService
    {
        Task<ScheduledPayment> CreateScheduledPaymentAsync(ScheduledPayment scheduledPayment);
        Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(
            string memberPaymentId = null,
            decimal? paymentAmount = null,
            bool? fulfiled = null,
            int? paymentMethod = null,
            string note = null,
            DateTimeOffset? paymentDueStartDate = null,
            DateTimeOffset? paymentDueEndtDate = null,
            DateTimeOffset? fulfiledStartDate = null,
            DateTimeOffset? fulfiledEndtDate = null);
        Task<ScheduledPayment> GetScheduledPaymentAsync(string scheduledPaymentId);
        Task<ScheduledPayment> UpdateScheduledPaymentAsync(ScheduledPayment scheduledPayment);
        Task<bool> DeleteScheduledPaymentAsync(string scheduledPaymentId);
    }
}
