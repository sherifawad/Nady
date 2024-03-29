﻿using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
using DataBase.UnitOfWork;
using Infrastructure.Dtos;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nady.Errors;
using Nady.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nady.Controllers
{
    /// <summary>
    /// Members Controllers
    /// </summary>
    [Authorize(Policy = "ModeratePhotoRole")]
    public class MembersController : BaseApiController
    {
        private readonly IMemberService _memberService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memberService"></param>
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<MemberDto>> GetMembers(
            [FromQuery]string memberName = null,
            [FromQuery]string code = null,
            [FromQuery]bool? isowner = null,
            [FromQuery]int? status = null,
            [FromQuery]string relationshop = null,
            [FromQuery] string note = null)
        {

            var members = (await _memberService.GetMembersAsync(memberName, code, isowner, status, relationshop, note))
                .Select( x => x.AsDto());

            return members;

            //Logger.LogInfromation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {members}");
        }

        /// <summary>
        /// Get memberBy Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberDto>> GetMemberById(string id)
        {
            var member = await _memberService.GetMemberAsync(id);

            if (member == null) return NotFound(new ApiResponse(404));

            return member.AsDto();
        }

        /// <summary>
        /// Get last member
        /// </summary>
        /// <returns></returns>
        [HttpGet("last")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberDto>> GetLastmemberAsync()
        {
            var member = await _memberService.GetLastMember();

            if (member == null) return NotFound(new ApiResponse(404));

            return member.AsDto();
        }

        /// <summary>
        /// Create A member
        /// </summary>
        /// <param name="memberDto"></param>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="total"></param>
        /// <param name="tax"></param>
        /// <param name="discount"></param>
        /// <param name="note"></param>
        /// <param name="scheduledpaymenamount"></param>
        /// <param name="scheduledevery"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MemberDto>> CreateMember([FromBody] MemberDto memberDto, 
            [FromQuery] int? type,
            [FromQuery] int? method,
            [FromQuery] decimal total,
            [FromQuery] double? tax,
            [FromQuery] double? discount,
            [FromQuery] string note,
            [FromQuery] decimal scheduledpaymenamount,
            [FromQuery] int scheduledevery
            )
        {

            var memberToCreate = memberDto.FromDto();
            var createdMember = await _memberService.CreateMemberAsync(
                memberToCreate,
                type,
                method,
                total,
                tax,
                discount,
                note,
                scheduledpaymenamount,
                scheduledevery,
                User.GetUserId());
            if (createdMember == null)
                return BadRequest("Failed to Add Member");


            return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.Id }, createdMember.AsDto());

        }

        /// <summary>
        /// Update a member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpadateMember(string id, [FromBody] MemberDto memberDto)
        {
            if (memberDto.Id != id) return BadRequest("Failed to update");
            var memberToUpdate = await _memberService.GetMemberAsync(id);
            if (memberToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedMember = await _memberService.UpdateMemberAsync(memberDto.FromDto(),
                User.GetUserId());
            if (updatedMember != null) return NoContent();

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a member and it's payments, histories and if is owner delete folloe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var result = await _memberService.DeleteMemberAsync(id,
                User.GetUserId());
            if (result) return NoContent();

            return BadRequest("Problem deleting the member");
        }
    }
}
