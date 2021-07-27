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
    public class MemberPaymentService : IMemberPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberPaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MemberPayment> CreatePaymentAsync(string memberId, string name, PaymentType paymentType, DateTime date, decimal total, decimal paymentAmount, bool isScheduled = false, double taxPercentage = 0, double discountPercentage = 0, string note = null)
        {
            var payment = new MemberPayment { MemberId = memberId, DiscountPercentage = discountPercentage, IsScheduled = isScheduled, Name = name, Note = note, PaymentAmount = paymentAmount, PaymentTotal = total, TaxPercentage = taxPercentage };
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

        public async Task<IReadOnlyList<MemberPayment>> GetMemberPaymentsAsync(string memberId)
        {
            var payments = await _unitOfWork.Repository<MemberPayment>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.Date));

            if (payments == null) return null;

            return payments;
        }

        public async Task<MemberPayment> GetPaymentAsync(string paymentId)
        {
            var paymen = await _unitOfWork.Repository<MemberPayment>().GetFirstOrDefault(x => x.Id == paymentId);

            if (paymen == null) return null;

            return paymen;
        }

        public async Task<IReadOnlyList<MemberPayment>> GetPaymentsAsync()
        {
            return await _unitOfWork.Repository<MemberPayment>().GetAllAsync();
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
