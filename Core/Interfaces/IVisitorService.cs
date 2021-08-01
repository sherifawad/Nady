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
        Task<MemberVisitor> GetVisitorAsync(string visitorId);
        Task<MemberVisitor> UpdateVisitorAsync(MemberVisitor visitor);
        Task<bool> DeleteVisitorAsync(string visitorId);
        Task<IReadOnlyList<MemberVisitor>> CreateVisitorsAsync(MemberVisitor visitor,
            decimal singleVisitorPrice,
            double? discount,
            double? tax,
            int? method,
            string note,
            int count = 1);
        Task<IReadOnlyList<MemberVisitor>> GetVisitorsAsync(string note = null,
            string memberId = null,
            int? type = null,
            int? status = null,
            DateTimeOffset? addedStart = null,
            DateTimeOffset? addedEnd = null,
            DateTimeOffset? accessedStart = null,
            DateTimeOffset? accessedEnd = null,
            int? gate = null);
    }
}
