using Core.Interfaces;
using Core.Models;
using DataBase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MemberHistoryService : IMemberHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MemberHistory> CreateHistoryAsync(MemberHistory history)
        {
            await _unitOfWork.Repository<MemberHistory>().AddItemAsync(history);
            // save to db
            if (await _unitOfWork.Complete()) return history;
            return null;
        }

        public async Task<bool> DeleteHistoryAsync(string historyId)
        {
            var history = await _unitOfWork.Repository<MemberHistory>().GetFirstOrDefault(x => x.Id == historyId);
            if (history == null) return false;
            await _unitOfWork.Repository<MemberHistory>().DeleteItemAsync(history);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<IReadOnlyList<MemberHistory>> GetHistoriesAsync(string memberId, string title, DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            return await _unitOfWork.Repository<MemberHistory>().Get(x => 
            (!string.IsNullOrWhiteSpace(memberId) ?  x.MemberId == memberId : true) &&
            (!string.IsNullOrWhiteSpace(title) ? x.Title.ToLower().Contains(title.ToLower()) : true)  &&
            (startDate != null ? x.Date >= startDate : true) &&
            (endDate != null ? x.Date <= endDate : true) ,
            orderBy: x => x.OrderBy(y => y.Date), track: false);

        }

        public async Task<MemberHistory> GetHistoryAsync(string historyId)
        {
            var history = await _unitOfWork.Repository<MemberHistory>().GetFirstOrDefault(x => x.Id == historyId, track:false);

            if (history == null) return null;

            return history;
        }

        public async Task<MemberHistory> UpdateHistoryAsync(MemberHistory history)
        {
            await _unitOfWork.Repository<MemberHistory>().UpdateItemAsync(history);
            // save to db
            if (await _unitOfWork.Complete()) return history;

            return null;
        }
    }
}
