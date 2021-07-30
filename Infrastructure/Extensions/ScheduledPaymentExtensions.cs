using Core.Models;
using Core.Models.Enum;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class ScheduledPaymentExtensions
    {
        public static ScheduledPaymentDto AsDto(this ScheduledPayment payment)
        {
            return new ScheduledPaymentDto
            {
                Id = payment.Id,
                Fulfiled = payment.Fulfiled,
                FulfiledDate = payment.FulfiledDate,
                MemberPaymentId = payment.MemberPaymentId,
                Note = payment.Note,
                PaymentAmount = payment.PaymentAmount,
                PaymentDueDate = payment.PaymentDueDate,
                PaymentMethod = ((int)payment.PaymentMethod),                
            };
        }
        public static ScheduledPayment FromDto(this ScheduledPaymentDto payment)
        {
            return new ScheduledPayment
            {
                Id = payment.Id,
                Fulfiled = payment.Fulfiled,
                FulfiledDate = payment.FulfiledDate,
                MemberPaymentId = payment.MemberPaymentId,
                Note = payment.Note,
                PaymentAmount = payment.PaymentAmount,
                PaymentDueDate = payment.PaymentDueDate,
                PaymentMethod = ((PaymentMethod)payment.PaymentMethod),                
            };
        }
    }
}
