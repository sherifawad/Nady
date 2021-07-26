using DataBase.Models;
using DataBase.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Nady.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nady.Controllers
{
    public class MembersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/<MembersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            throw new ArgumentException("Test Exception");
            return Ok(await _unitOfWork.Repository<Member>().GetAllAsync());
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(string id)
        {
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == id, $"{nameof(Member.MemberDetails)},{nameof(Member.MemberHistory)},{nameof(Member.MemberPayments)}");

            if (member == null) return NotFound(new ApiResponse(404));

            return member;
        }

        // GET api/<MembersController>/5
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMemberByName(string name)
        {
            var members = await _unitOfWork.Repository<Member>().Get(x => x.Name.ToLower().Contains(name.ToLower()), orderBy: x => x.OrderBy(y => y.Name));

            if (members == null) return NotFound(new ApiResponse(404));

            return members;
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] Member value)
        {
            var result = await _unitOfWork.Repository<Member>().AddItemAsync(value, false);
            if (await _unitOfWork.Complete()) 
                return Created("", result);

            return BadRequest("Failed to Add Member");

        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpadateMember(string id, [FromBody] Member value)
        {
            var member = await _unitOfWork.Repository<Member>().GetFirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound(new ApiResponse(404));
            await _unitOfWork.Repository<Member>().UpdateItemAsync(value, false);
            if (await _unitOfWork.Complete()) return Ok(value);

            return BadRequest("Failed to update");
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
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
