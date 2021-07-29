using Core.Models;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class MemberExtensions
    {
        public static MemberDto AsDto(this Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Address = member.MemberDetails?.Address,
                Code = member.Code,
                Image = member.MemberDetails?.Image,
                IsOwner = member.IsOwner,
                MemberStatus = member.MemberStatus,
                NickName = member.MemberDetails?.NickName,
                Note = member.Note,
                Phone = member.MemberDetails?.Phone,
                RelationShip = member.RelationShip

            };
        }
        public static Member FromDto(this MemberDto memberDto)
        {
            return new Member
            {
                Id = memberDto.Id,
                Name = memberDto.Name,
                Code = memberDto.Code,
                IsOwner = memberDto.IsOwner,
                MemberStatus = memberDto.MemberStatus,
                Note = memberDto.Note,
                RelationShip = memberDto.RelationShip,
                MemberDetails = new MemberDetails
                {
                    Address = memberDto.Address,
                    Image = memberDto.Image,
                    NickName = memberDto.NickName,
                    Phone = memberDto.Phone,

                }

            };
        }
    }
}
