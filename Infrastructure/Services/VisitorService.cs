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
        public async Task<MemberVisitor> CreateVisitoAsync(string memberId, VisitorType visitorType, Gate gate, bool used = false, DateTime? accessDate = null, string note = null)
        {
            var visitor = new MemberVisitor { MemberId = memberId, AccessesDate = accessDate, Gate = gate, Note = note, Used = used, VisitorType = visitorType};
            await _unitOfWork.Repository<MemberVisitor>().AddItemAsync(visitor);
            // save to db
            if (await _unitOfWork.Complete()) return visitor;
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

        public async Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresAsync(string memberId)
        {
            var visitores = await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.AccessesDate));

            if (visitores == null) return null;

            return visitores;
        }

        public async Task<MemberVisitor> GetVisitorAsync(string visitorId)
        {
            var visitor = await _unitOfWork.Repository<MemberVisitor>().GetFirstOrDefault(x => x.Id == visitorId);

            if (visitor == null) return null;

            return visitor;
        }

        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync()
        {
            return await _unitOfWork.Repository<MemberVisitor>().GetAllAsync();
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
