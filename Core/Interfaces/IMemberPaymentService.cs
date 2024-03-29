﻿using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberPaymentService
    {
        Task<MemberPayment> CreatePaymentAsync(MemberPayment payment);
        Task<IReadOnlyList<MemberPayment>> GetPaymentsAsync(
            string memberId = null,
            string name = null,
            string note = null,
            int? paymentType = null,
            int? paymentMethod = null,
            decimal? paymentTotal = null,
            double? taxPercentage = null,
            double? discountPercentage = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null);
        Task<MemberPayment> GetPaymentAsync(string paymentId);
        Task<MemberPayment> UpdatePaymentAsync(MemberPayment payment);
        Task<bool> DeletePaymentAsync(string paymentId);
    }
}
