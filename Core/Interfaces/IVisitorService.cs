using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVisitorService
    {
        Task<MemberVisitor> CreateVisitorAsync(string memberId, VisitorType visitorType, Gate gate, VisitorStatus status = VisitorStatus.UnUsed, DateTime? accessDate = null, string note = null);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsByTypeAsync(int type = 0);
        Task<MemberVisitor> GetVisitorAsync(string visitorId);
        Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresByTypeAsync(string memberId, int type = 0);
        Task<MemberVisitor> UpdateVisitorAsync(MemberVisitor visitor);
        Task<bool> DeleteVisitorAsync(string visitorId);
        Task<IReadOnlyList<MemberVisitor>> CreateVisitorsAsync(string memberId, VisitorType visitorType, int count, string note = null);
        Task<bool> DeleteVisitorsAsync(string memberId, int type = 0, int status = 0);
        Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresByStatusAsync(string memberId, int status = 0);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsByStatusAsync(int status = 0);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsWithoutMemberAsync(int type = 0, int status = 0);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync(string memberId = null, int type = 0, int status = 0);
    }
}
