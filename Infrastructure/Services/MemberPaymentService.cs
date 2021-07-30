﻿using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
using DataBase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MemberPaymentService : IMemberPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberPaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MemberPayment> CreatePaymentAsync(MemberPayment payment)
        {
            await _unitOfWork.Repository<MemberPayment>().AddItemAsync(payment);
            // save to db
            if (await _unitOfWork.Complete()) return payment;
            return null;
        }

        public async Task<bool> DeletePaymentAsync(string paymentId)
        {
            var payment = await _unitOfWork.Repository<MemberPayment>().GetFirstOrDefault(x => x.Id == paymentId);
            if (payment == null) return false;
            await _unitOfWork.Repository<MemberPayment>().DeleteItemAsync(payment);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<MemberPayment> GetPaymentAsync(string paymentId)
        {
            var paymen = await _unitOfWork.Repository<MemberPayment>().GetFirstOrDefault(x => x.Id == paymentId, track: false);

            if (paymen == null) return null;

            return paymen;
        }

        public async Task<IReadOnlyList<MemberPayment>> GetPaymentsAsync(
            string memberId = null,
            string name = null,
            bool? isScheduled = null,
            int? paymentType = null,
            decimal? paymentAmount = null,
            decimal? paymentTotal = null,
            double? taxPercentage = null,
            double? discountPercentage = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null)
        {
            return await _unitOfWork.Repository<MemberPayment>().Get(x =>
                (!string.IsNullOrWhiteSpace(memberId) ? x.MemberId == memberId : true) &&
                (!string.IsNullOrWhiteSpace(name) ? x.Name.ToLower().Contains(name.ToLower()) : true) &&
                (isScheduled != null ? x.IsScheduled == isScheduled : true) &&
                (paymentType != null ? x.PaymentType == (PaymentType)paymentType : true) &&
                (paymentAmount != null ? x.PaymentAmount == paymentAmount : true) &&
                (paymentTotal != null ? x.PaymentTotal == paymentTotal : true) &&
                (taxPercentage != null ? x.TaxPercentage == taxPercentage : true) &&
                (discountPercentage != null ? x.DiscountPercentage == discountPercentage : true) &&
                (startDate != null ? x.Date >= startDate : true) &&
                (endDate != null ? x.Date <= endDate : true), 
                orderBy: x => x.OrderBy(y => y.Date), track: false);
        }

        public async Task<MemberPayment> UpdatePaymentAsync(MemberPayment payment)
        {
            await _unitOfWork.Repository<MemberPayment>().UpdateItemAsync(payment);
            // save to db
            if (await _unitOfWork.Complete()) return payment;

            return null;
        }
    }
}
