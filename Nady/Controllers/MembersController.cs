using Core.Interfaces;
using Core.Models;
using DataBase.UnitOfWork;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nady.Errors;
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
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            return Ok(await _memberService.GetMembersAsync());
        }

        /// <summary>
        /// Get memberBy Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Member>> GetMemberById(string id)
        {
            var member = await _memberService.GetMemberAsync(id);

            if (member == null) return NotFound(new ApiResponse(404));

            return member;
        }

        /// <summary>
        /// Get memeber by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMemberByName(string name)
        {
            var members = await _memberService.GetMembersByNameAsync(name);

            if (members == null) return NotFound(new ApiResponse(404));

            return Ok(members);
        }

        /// <summary>
        /// Create a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMember([FromBody] Member member)
        {
            var result = await _memberService.CreateMemberAsync(member.Name, member.IsOwner, member.Code, member.RelationShip);
            if (result != null) 
                return Created("", result);

            return BadRequest("Failed to Add Member");

        }

        /// <summary>
        /// Update a member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpadateMember(string id, [FromBody] Member value)
        {
            var member = await _memberService.GetMemberAsync(id);
            if (member == null) return NotFound(new ApiResponse(404));
            var result = await _memberService.UpdateMemberAsync(value);
            if (result != null) return Ok(result);

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var result = await _memberService.DeleteMemberAsync(id);
            if (result) return Ok(id);

            return BadRequest("Problem deleting the message");
        }
    }
}
