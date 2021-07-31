using Core.Interfaces;
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
    public class ScheduledPaymentService : IScheduledPaymentService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ScheduledPaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ScheduledPayment> CreateScheduledPaymentAsync(ScheduledPayment scheduledPayment)
        {
            await _unitOfWork.Repository<ScheduledPayment>().AddItemAsync(scheduledPayment);
            // save to db
            if (await _unitOfWork.Complete()) return scheduledPayment;
            return null;
        }

        public async Task<bool> DeleteScheduledPaymentAsync(string scheduledPaymentId)
        {
            var scheduledPayment = await _unitOfWork.Repository<ScheduledPayment>().GetFirstOrDefault(x => x.Id == scheduledPaymentId);
            if (scheduledPayment == null) return false;
            await _unitOfWork.Repository<ScheduledPayment>().DeleteItemAsync(scheduledPayment);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<ScheduledPayment> GetScheduledPaymentAsync(string scheduledPaymentId)
        {
            var paymen = await _unitOfWork.Repository<ScheduledPayment>().GetFirstOrDefault(x => x.Id == scheduledPaymentId, track:false);

            if (paymen == null) return null;

            return paymen;
        }

        public async Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(
            string memberPaymentId = null,
            decimal? paymentAmount = null,
            bool? fulfiled = null,
            int? paymentMethod = null,
            string note = null,
            DateTimeOffset? paymentDueStartDate = null,
            DateTimeOffset? paymentDueEndtDate = null,
            DateTimeOffset? fulfiledStartDate = null,
            DateTimeOffset? fulfiledEndtDate = null
            )
        {
            return await _unitOfWork.Repository<ScheduledPayment>().Get(x =>
                (!string.IsNullOrWhiteSpace(memberPaymentId) ? x.MemberPaymentId == memberPaymentId : true) &&
                (!string.IsNullOrWhiteSpace(note) ? x.Note.ToLower().Contains(note.ToLower()) : true) &&
                (fulfiled != null ? x.Fulfiled == fulfiled : true) &&
                (paymentMethod != null ? x.PaymentMethod == (PaymentMethod)paymentMethod : true) &&
                (paymentAmount != null ? x.PaymentAmount == paymentAmount : true) &&
                (paymentDueStartDate != null ? x.PaymentDueDate >= paymentDueStartDate : true) &&
                (paymentDueEndtDate != null ? x.PaymentDueDate <= paymentDueEndtDate : true)&&
                (fulfiledStartDate != null ? x.FulfiledDate >= fulfiledStartDate : true) &&
                (fulfiledEndtDate != null ? x.FulfiledDate <= fulfiledEndtDate : true),
                orderBy: x => x.OrderBy(y => y.PaymentDueDate).ThenBy(y => y.FulfiledDate), track: false);
        }

        public async Task<ScheduledPayment> UpdateScheduledPaymentAsync(ScheduledPayment scheduledPayment)
        {
            await _unitOfWork.Repository<ScheduledPayment>().UpdateItemAsync(scheduledPayment);
            // save to db
            if (await _unitOfWork.Complete()) return scheduledPayment;

            return null;
        }
    }
}
