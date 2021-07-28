﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberHistoryService
    {
        Task<MemberHistory> CreateHistoryAsync(string memberId, string title, DateTime date, string detail = null);
        Task<IReadOnlyList<MemberHistory>> GetHistoriesAsync();
        Task<MemberHistory> GetHistoryAsync(string historyId);
        Task<IReadOnlyList<MemberHistory>> GetMemberHistoriesAsync(string memberId);
        Task<MemberHistory> UpdateHistoryAsync(MemberHistory history);
        Task<bool> DeleteMemberAsync(string historyId);
    }
}