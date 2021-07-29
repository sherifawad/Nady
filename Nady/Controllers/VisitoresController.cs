using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
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
    /// Visitor controller
    /// </summary>
    public class VisitoresController : BaseApiController
    {

        private readonly IVisitorService _visitorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="visitorService"></param>
        public VisitoresController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        /// <summary>
        /// Get visitors 
        /// </summary>
        /// <param name="memberId">Member Id</param>
        /// <param name="type">0 all, 1 Free, 2 Paid</param>
        /// <param name="status">0 all, 1 used, 2 unused 3 suspend</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<MemberVisitor>> GetVisitores([FromQuery] string memberId = null , [FromQuery] int type = 0, [FromQuery] int status = 0)
        {
            return await _visitorService.GetVisitorsAsync(memberId, type, status);
                
        }

        /// <summary>
        /// Get specific visitor by id
        /// </summary>
        /// <param name="id">visitor id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberVisitor>> GetVisitorById(string id)
        {
            var visitor = await _visitorService.GetVisitorAsync(id);

            if (visitor == null) return NotFound(new ApiResponse(404));

            return visitor;
        }

        /// <summary>
        /// create Visitors
        /// </summary>
        /// <param name="visitor">visitor to create</param>
        /// <param name="count">number of visitors to create</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MemberVisitor>>> CreateVisitors([FromBody] MemberVisitor visitor, [FromQuery]int count = 1)
        {
            if(count > 1)
            {
                var createdVisitores = await _visitorService.CreateVisitorsAsync(visitor.MemberId, visitor.VisitorType, count, visitor.Note);
                if (createdVisitores == null)
                    return BadRequest("Failed to Create visitors");


                return CreatedAtAction(nameof(GetVisitores), new { memberId = visitor.MemberId, type = ((int)visitor.VisitorType) }, createdVisitores);
            }
            else
            {
                var createdVisitor = await _visitorService.CreateVisitorAsync(visitor.MemberId, visitor.VisitorType, visitor.Gate, visitor.VisitorStatus, visitor.AccessesDate, visitor.Note);
                if (createdVisitor == null)
                    return BadRequest("Failed to Create a visitor");


                return CreatedAtAction(nameof(GetVisitorById), new { id = createdVisitor.Id }, createdVisitor);
            }

        }

        /// <summary>
        /// Update Visitor
        /// </summary>
        /// <param name="id"> visitor to update id</param>
        /// <param name="visitor">visitor to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upadatevisitor(string id, [FromBody] MemberVisitor visitor)
        {
            if(visitor.Id != id) return BadRequest("Failed to update");
            var visitorToUpdate = await _visitorService.GetVisitorAsync(id);
            if (visitorToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedVisitor = await _visitorService.UpdateVisitorAsync(visitor);
            if (updatedVisitor != null) return NoContent();

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete visitor
        /// </summary>
        /// <param name="id">visitor id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _visitorService.DeleteVisitorAsync(id);
            if (result) return NoContent();

            return BadRequest("Problem deleting the visitor");
        }

        /// <summary>
        /// Delete member visitors
        /// </summary>
        /// <param name="memberId">member id</param>
        /// <param name="type">visitor type => 0 all, 1 Free, 2 Paid</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string memberId, int type = 0)
        {
            var result = await _visitorService.DeleteMemberVisitorAsync(memberId, type);
            if (result) return NoContent();

            return BadRequest("Problem deleting the member visitores");
        }
    }
}
