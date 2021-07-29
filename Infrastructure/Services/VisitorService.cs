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

        public async Task<MemberVisitor> CreateVisitorAsync(string memberId, VisitorType visitorType, Gate gate, VisitorStatus status = VisitorStatus.UnUsed, DateTime? accessDate = null, string note = null)
        {
            var member = await _unitOfWork.Repository<Member>().FindAsync(memberId);
            if (member is null) return null;

            var visitor = new MemberVisitor { MemberId = memberId, AccessesDate = accessDate, Gate = gate, Note = note, VisitorStatus = status, VisitorType = visitorType};
            await _unitOfWork.Repository<MemberVisitor>().AddItemAsync(visitor);
            // save to db
            if (await _unitOfWork.Complete()) return visitor;
            return null;
        }
        public async Task<IReadOnlyList<MemberVisitor>> CreateVisitorsAsync(string memberId, VisitorType visitorType, int count,string note = null)
        {
            var member = await _unitOfWork.Repository<Member>().FindAsync(memberId);
            if (member is null) return null;
            var list = new List<MemberVisitor>();

            for (int i = 0; i < count; i++)
            {
                var visitor = new MemberVisitor { MemberId = memberId, Note = note, VisitorType = visitorType, VisitorStatus = VisitorStatus.UnUsed};
                await _unitOfWork.Repository<MemberVisitor>().AddItemAsync(visitor);
                list.Add(visitor);
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

        public async Task<bool> DeleteMemberVisitorAsync(string memberId, int type = 0)
        {

            var memberVisitors = type switch
            {
                0 => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: y => y.OrderBy(y => y.AccessesDate)),
                _ => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId && ((int)x.VisitorType) == type, orderBy: y => y.OrderBy(y => y.AccessesDate))
            };

            if (memberVisitors == null) return false;

            foreach (var visitor in memberVisitors)
            {
                await _unitOfWork.Repository<MemberVisitor>().DeleteItemAsync(visitor);
            }
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresByTypeAsync(string memberId, int type = 0)
        {
            return type switch
            {
                0 => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: y => y.OrderBy(y => y.AccessesDate)),
                _ => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId && ((int)x.VisitorType) == type, orderBy: y => y.OrderBy(y => y.AccessesDate))
            };
        }
        public async Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresByStatusAsync(string memberId, int status = 0)
        {
            return status switch
            {
                0 => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: y => y.OrderBy(y => y.AccessesDate)),
                _ => await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId && ((int)x.VisitorStatus) == status, orderBy: y => y.OrderBy(y => y.AccessesDate))
            };
        }

        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsByStatusAsync(int status = 0)
        {
            return status switch
            {
                0 => await _unitOfWork.Repository<MemberVisitor>().Get(orderBy: y => y.OrderBy(y => y.AccessesDate)),
                _ => await _unitOfWork.Repository<MemberVisitor>().Get(x => ((int)x.VisitorStatus) == status, orderBy: y => y.OrderBy(y => y.AccessesDate))
            };

        }

        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsByTypeAsync(int type = 0)
        {
            return type switch
            {
                0 => await _unitOfWork.Repository<MemberVisitor>().Get(orderBy: y => y.OrderBy(y => y.AccessesDate)),
                _ => await _unitOfWork.Repository<MemberVisitor>().Get(x => ((int)x.VisitorType) == type, orderBy: y => y.OrderBy(y => y.AccessesDate))
            };

        }
        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsWithoutMemberAsync(int type = 0, int status = 0)
        {
            if (type != 0 && status == 0)
                return await GetVisitorsByTypeAsync(type);
            else if (type == 0 && status != 0)
                return await GetVisitorsByStatusAsync(status);
            else if (type != 0 && status != 0)
                return await _unitOfWork.Repository<MemberVisitor>().Get(x => ((int)x.VisitorStatus) == status && ((int)x.VisitorType) == type,
                    orderBy: y => y.OrderBy(y => y.AccessesDate));
            else if(type == 0 && status == 0)
                return await _unitOfWork.Repository<MemberVisitor>().GetAllAsync();

            return null;

        }

        public async Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync(string memberId = null, int type = 0, int status = 0)
        {
            if (!string.IsNullOrWhiteSpace(memberId) && type != 0 && status != 0)
                return await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId && ((int)x.VisitorStatus) == status && ((int)x.VisitorType) == type,
                    orderBy: y => y.OrderBy(y => y.AccessesDate));

            else if (!string.IsNullOrWhiteSpace(memberId) && type != 0 && status == 0)
                return await GetMemberVisitoresByTypeAsync(memberId, type);

            else if (!string.IsNullOrWhiteSpace(memberId) && type == 0 && status != 0)
                return await GetMemberVisitoresByStatusAsync(memberId, status);

            else if (!string.IsNullOrWhiteSpace(memberId) && type == 0 && status == 0)
                return await _unitOfWork.Repository<MemberVisitor>().Get(x => x.MemberId == memberId, orderBy: y => y.OrderBy(y => y.AccessesDate));

            else if (string.IsNullOrWhiteSpace(memberId))
                return await GetVisitorsWithoutMemberAsync(type, status);

            return null;
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
