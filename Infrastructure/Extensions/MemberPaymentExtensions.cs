﻿using Core.Models;
using Core.Models.Enum;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class MemberPaymentExtensions
    {
        public static MemberPaymentDto AsDto(this MemberPayment payment)
        {
            return new MemberPaymentDto
            {
                Date = payment.Date,
                DiscountPercentage = payment.DiscountPercentage,
                Id = payment.Id,
                MemberId = payment.MemberId,
                Name = payment.Name,
                Note = payment.Note,
                PaymentTotal = payment.PaymentTotal,
                PaymentType = ((int)payment.PaymentType),
                PaymentMethod = ((int?)payment.PaymentMethod),
                TaxPercentage = payment.TaxPercentage
            };
        }
        public static MemberPayment FromDto(this MemberPaymentDto payment)
        {
            return new MemberPayment
            {
                Date = payment.Date,
                DiscountPercentage = payment.DiscountPercentage,
                Id = payment.Id,
                MemberId = payment.MemberId,
                Name = payment.Name,
                Note = payment.Note,
                PaymentTotal = payment.PaymentTotal,
                PaymentType = (PaymentType)payment.PaymentType,
                PaymentMethod = ((PaymentMethod?)payment.PaymentMethod),
                TaxPercentage = payment.TaxPercentage
            };
        }
    }
}
