using Core.Models;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class MemberHistoryExtensions
    {
        public static MemberHistoryDto AsDto(this MemberHistory memberHistory)
        {
            return new MemberHistoryDto
            {
                Date = memberHistory.Date,
                Detail = memberHistory.Detail,
                Id = memberHistory.Id,
                MemberId = memberHistory.MemberId,
                Title = memberHistory.Title
            };
        }

        public static MemberHistory FromDto(this MemberHistoryDto memberHistory)
        {
            return new MemberHistory
            {
                Date = memberHistory.Date,
                Detail = memberHistory.Detail,
                Id = memberHistory.Id,
                MemberId = memberHistory.MemberId,
                Title = memberHistory.Title
            };
        }
    }
}
