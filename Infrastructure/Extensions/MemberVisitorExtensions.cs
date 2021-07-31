using Core.Models;
using Core.Models.Enum;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class MemberVisitorExtensions
    {
        public static MemberVisitorDto AsDto(this MemberVisitor visitor)
        {
            return new MemberVisitorDto
            {
                Id = visitor.Id,
                Note = visitor.Note,
                AccessesDate = visitor.AccessesDate,
                Gate = (int?)visitor.Gate.Value,
                MemberId = visitor.MemberId,
                VisitorStatus = (int)visitor.VisitorStatus,
                VisitorType = (int)visitor.VisitorType

            };
        }
        public static MemberVisitor FromDto(this MemberVisitorDto visitor)
        {
            return new MemberVisitor
            {
                Id = visitor.Id,
                Note = visitor.Note,
                AccessesDate = visitor.AccessesDate,
                Gate = ((Gate?)visitor.Gate.Value),
                MemberId = visitor.MemberId,
                VisitorStatus = (VisitorStatus)visitor.VisitorStatus,
                VisitorType = (VisitorType)visitor.VisitorType

            };
        }
    }
}
