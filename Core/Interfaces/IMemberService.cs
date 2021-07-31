﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemberService
    {
        Task<IReadOnlyList<Member>> GetMembersAsync(string memberName = null, string code = null);
        Task<Member> GetMemberAsync(string memberId);
        Task<Member> UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(string memberId);
        Task<Member> CreateMemberAsync(Member memberToCreate, int type, int? method, decimal total, double tax, double discount, DateTimeOffset date, string note, decimal scheduledpaymenamount, int scheduledevery);
    }
}
