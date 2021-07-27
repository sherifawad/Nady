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
        Task<MemberVisitor> CreateVisitoAsync(string memberId, VisitorType visitorType, Gate gate, bool used = false, DateTime? accessDate = null, string note = null);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync();
        Task<MemberVisitor> GetVisitorAsync(string visitorId);
        Task<IReadOnlyList<MemberVisitor>> GetMemberVisitoresAsync(string memberId);
        Task<MemberVisitor> UpdateVisitorAsync(MemberVisitor visitor);
        Task<bool> DeleteVisitorAsync(string visitorId);
    }
}
