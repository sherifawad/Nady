using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
using Infrastructure.Dtos;
using Infrastructure.Extensions;
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
        /// <param name="note"></param>
        /// <param name="memberId"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="addedStart"></param>
        /// <param name="addedEnd"></param>
        /// <param name="accessedStart"></param>
        /// <param name="accessedEnd"></param>
        /// <param name="gate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<MemberVisitorDto>> GetVisitoresAsync(
            [FromQuery]string note = null,
            [FromQuery]string memberId = null,
            [FromQuery]int? type = null,
            [FromQuery]int? status = null,
            [FromQuery]DateTimeOffset? addedStart = null,
            [FromQuery]DateTimeOffset? addedEnd = null,
            [FromQuery]DateTimeOffset? accessedStart = null,
            [FromQuery]DateTimeOffset? accessedEnd = null,
            [FromQuery] int? gate = null
            )
        {
            return (await _visitorService.GetVisitorsAsync(note, memberId, type, status, addedStart, addedEnd, accessedStart, accessedEnd, gate)).Select(x => x.AsDto());                
        }

        /// <summary>
        /// Get specific visitor by id
        /// </summary>
        /// <param name="id">visitor id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberVisitorDto>> GetVisitorByIdAsync(string id)
        {
            var visitor = await _visitorService.GetVisitorAsync(id);

            if (visitor == null) return NotFound(new ApiResponse(404));

            return visitor.AsDto();
        }

        /// <summary>
        /// create Visitors
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="singleVisitorPrice"></param>
        /// <param name="discount"></param>
        /// <param name="tax"></param>
        /// <param name="method"></param>
        /// <param name="note"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MemberVisitorDto>>> CreateVisitorsAsync(
            [FromBody] MemberVisitorDto visitor,
            [FromQuery]decimal singleVisitorPrice,
            [FromQuery]double? discount,
            [FromQuery]double? tax,
            [FromQuery]int? method,
            [FromQuery] string note,
            [FromQuery] int count = 1
            )
        {
            var createdVisitores = await _visitorService.CreateVisitorsAsync(visitor.FromDto(), singleVisitorPrice, discount, tax, method, note, count);
            if (createdVisitores == null)
                return BadRequest("Failed to Create visitors");
            if (count > 1)
                return CreatedAtAction(nameof(GetVisitoresAsync), new { memberId = visitor.MemberId, type = ((int)visitor.VisitorType), status = ((int)visitor.VisitorStatus) }, createdVisitores);
            else
                return CreatedAtAction(nameof(GetVisitorByIdAsync), new { id = createdVisitores[0].Id }, createdVisitores[0].AsDto());

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
        public async Task<IActionResult> Upadatevisitor(string id, [FromBody] MemberVisitorDto visitor)
        {
            if(visitor.Id != id) return BadRequest("Failed to update");
            var visitorToUpdate = await _visitorService.GetVisitorAsync(id);
            if (visitorToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedVisitor = await _visitorService.UpdateVisitorAsync(visitor.FromDto());
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
    }
}
