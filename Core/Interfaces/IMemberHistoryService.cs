using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberHistoryService
    {
        Task<MemberHistory> CreateHistoryAsync(MemberHistory history);
        Task<IReadOnlyList<MemberHistory>> GetHistoriesAsync(string memberId, string title, DateTimeOffset? startDate, DateTimeOffset? endDate);
        Task<MemberHistory> GetHistoryAsync(string historyId);
        Task<MemberHistory> UpdateHistoryAsync(MemberHistory history);
        Task<bool> DeleteHistoryAsync(string historyId);
    }
}
