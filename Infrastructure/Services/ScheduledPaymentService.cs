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
        public async Task<ScheduledPayment> CreateScheduledPaymentAsync(string memberPaymentId, string name, PaymentMethod paymentMethod, DateTime dueDate, decimal total, DateTime? fulfilledDate, bool fulfilled = false, string note = null)
        {
            var payment = new ScheduledPayment { MemberPaymentId = memberPaymentId, Fulfiled = fulfilled, FulfiledDate = fulfilledDate, PaymentMethod = paymentMethod, PaymentDueDate = dueDate};
            await _unitOfWork.Repository<ScheduledPayment>().AddItemAsync(payment);
            // save to db
            if (await _unitOfWork.Complete()) return payment;
            return null;
        }

        public async Task<bool> DeletePaymentAsync(string scheduledPaymentId)
        {
            var payment = await _unitOfWork.Repository<ScheduledPayment>().GetFirstOrDefault(x => x.Id == scheduledPaymentId);
            if (payment == null) return false;
            await _unitOfWork.Repository<ScheduledPayment>().DeleteItemAsync(payment);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<ScheduledPayment> GetScheduledPaymentAsync(string scheduledPaymentId)
        {
            var paymen = await _unitOfWork.Repository<ScheduledPayment>().GetFirstOrDefault(x => x.Id == scheduledPaymentId);

            if (paymen == null) return null;

            return paymen;
        }

        public async Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync()
        {
            return await _unitOfWork.Repository<ScheduledPayment>().GetAllAsync();
        }

        public async Task<IReadOnlyList<ScheduledPayment>> GetScheduledPaymentsAsync(string paymentId)
        {
            var payments = await _unitOfWork.Repository<ScheduledPayment>().Get(x => x.MemberPaymentId == paymentId, orderBy: x => x.OrderBy(y => y.PaymentDueDate).ThenBy(y => y.FulfiledDate));

            if (payments == null) return null;

            return payments;
        }

        public async Task<ScheduledPayment> UpdatePaymentAsync(ScheduledPayment scheduledPayment)
        {
            await _unitOfWork.Repository<ScheduledPayment>().UpdateItemAsync(scheduledPayment);
            // save to db
            if (await _unitOfWork.Complete()) return scheduledPayment;

            return null;
        }
    }
}
