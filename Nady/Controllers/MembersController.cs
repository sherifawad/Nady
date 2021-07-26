using DataBase.Models;
using DataBase.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            return Ok(await _unitOfWork.Repository<Member>().GetAllAsync());
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
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == id, $"{nameof(Member.MemberDetails)},{nameof(Member.MemberHistory)},{nameof(Member.MemberPayments)}");

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
        public async Task<ActionResult<IEnumerable<Member>>> GetMemberByName(string name)
        {
            var members = await _unitOfWork.Repository<Member>().Get(x => x.Name.ToLower().Contains(name.ToLower()), orderBy: x => x.OrderBy(y => y.Name));

            if (members == null) return NotFound(new ApiResponse(404));

            return members;
        }

        /// <summary>
        /// Create a new member
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMember([FromBody] Member value)
        {
            var result = await _unitOfWork.Repository<Member>().AddItemAsync(value, false);
            if (await _unitOfWork.Complete()) 
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
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound(new ApiResponse(404));
            await _unitOfWork.Repository<Member>().UpdateItemAsync(value, false);
            if (await _unitOfWork.Complete()) return Ok(value);

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound(new ApiResponse(404));
            await _unitOfWork.Repository<Member>().DeleteItemAsync(member, false);
            if (await _unitOfWork.Complete()) return Ok(id);

            return BadRequest("Problem deleting the message");
        }
    }
}
