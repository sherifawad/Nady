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

        public async Task<bool> DeleteMemberAsync(string historyId)
        {
            var history = await _unitOfWork.Repository<MemberHistory>().GetFirstOrDefault(x => x.Id == historyId);
            if (history == null) return false;
            await _unitOfWork.Repository<MemberHistory>().DeleteItemAsync(history);
            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<IReadOnlyList<MemberHistory>> GetHistoriesAsync()
        {
            return await _unitOfWork.Repository<MemberHistory>().GetAllAsync();
        }

        public async Task<MemberHistory> GetHistoryAsync(string historyId)
        {
            var history = await _unitOfWork.Repository<MemberHistory>().GetFirstOrDefault(x => x.Id == historyId);

            if (history == null) return null;

            return history;
        }

        public async Task<IReadOnlyList<MemberHistory>> GetMemberHistoriesAsync(string memberId)
        {
            var histories = await _unitOfWork.Repository<MemberHistory>().Get(x => x.MemberId == memberId, orderBy: x => x.OrderBy(y => y.Date));

            if (histories == null) return null;

            return histories;
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
