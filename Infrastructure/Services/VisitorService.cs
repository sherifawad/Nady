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
    public class VisitorService : IVisitorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VisitorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<MemberVisitor>> CreateVisitorsAsync(
            MemberVisitor visitor,
            decimal singleVisitorPrice,
            double? discount,
            double? tax,
            int? method,
            string note,
            int count = 1)
        {
            // Ensure count not less than 1
            count = count < 1 ? 1 : count;
            //check if member exist and the member is owner
            var member = await _unitOfWork.Repository<Member>().FindAsync(visitor?.MemberId);
            if (member is null || !member.IsOwner) return null;


            var list = new List<MemberVisitor>();

            for (int i = 0; i < count; i++)
            {
                await _unitOfWork.Repository<MemberVisitor>().AddItemAsync(visitor);
                list.Add(visitor);
            }
            // add Payment if visitor type is paid
            if (visitor.VisitorType == VisitorType.Paid)
            {
                var visitorPayment = new MemberPayment
                {
                    Date = DateTimeOffset.Now,
                    DiscountPercentage = discount,
                    TaxPercentage = tax,
                    PaymentType = PaymentType.One,
                    MemberId = member.Id,
                    Name = "Add visitors",
                    PaymentMethod = (PaymentMethod?)method,
                    PaymentTotal = singleVisitorPrice * count,
                    Note = note
                };
                foreach (var item in list)
                {
                    // Add visitor id to payment notes to ease delete payment and corresponding visitores
                    visitorPayment.Note = visitorPayment.Note is null ? $"Visitor Id: {item.Id}" : visitorPayment.Note + $"{ Environment.NewLine}Visitor Id: {item.Id}";

                }
                await _unitOfWork.Repository<MemberPayment>().AddItemAsync(visitorPayment);
            }

            // save to db
            if (await _unitOfWork.Complete()) return list;
            return null;
        }

        public async Task<bool> DeleteVisitorAsync(string visitorId)
        {
            var visitor = await _unitOfWork.Repository<MemberVisitor>().GetFirstOrDefault(x => x.Id == visitorId);
            if (visitor == null) return false;
            await _unitOfWork.Repository<MemberVisitor>().DeleteItemAsync(visitor);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync(
            string note = null,
            string memberId = null,
            int? type = null,
            int? status = null,
            DateTimeOffset? addedStart = null,
            DateTimeOffset? addedEnd = null,
            DateTimeOffset? accessedStart = null,
            DateTimeOffset? accessedEnd = null,
            int? gate = null

            )
        {
            return await _unitOfWork.Repository<MemberVisitor>().Get(x =>
                (!string.IsNullOrWhiteSpace(memberId) ? x.MemberId == memberId : true) &&
                (!string.IsNullOrWhiteSpace(note) ? x.Note.ToLower().Contains(note.ToLower()) : true) &&
                (gate != null ? x.Gate == (Gate)gate : true) &&
                (type != null ? x.VisitorType == (VisitorType)type : true) &&
                (status != null ? x.VisitorStatus == (VisitorStatus)status : true) &&
                (addedStart != null ? x.AddedDate >= addedStart : true) &&
                (addedEnd != null ? x.AddedDate <= addedEnd : true)&&
                (accessedStart != null ? x.AccessesDate >= accessedStart : true) &&
                (accessedEnd != null ? x.AccessesDate <= accessedEnd : true),
                orderBy: x => x.OrderBy(y => y.AddedDate).ThenBy(y => y.AccessesDate), track: false);
        }

        public async Task<MemberVisitor> GetVisitorAsync(string visitorId)
        {
            var visitor = await _unitOfWork.Repository<MemberVisitor>().GetFirstOrDefault(x => x.Id == visitorId, track: false);

            if (visitor == null) return null;

            return visitor;
        }


        public async Task<MemberVisitor> UpdateVisitorAsync(MemberVisitor visitor)
        {
            await _unitOfWork.Repository<MemberVisitor>().UpdateItemAsync(visitor);
            // save to db
            if (await _unitOfWork.Complete()) return visitor;

            return null;
        }
    }
}
