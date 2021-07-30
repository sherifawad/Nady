using Core.Interfaces;
using Core.Models;
using DataBase.UnitOfWork;
using Infrastructure.Dtos;
using Infrastructure.Extensions;
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
    public class HistoryController : BaseApiController
    {
        private readonly IMemberHistoryService _historyService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="historyService"></param>
        public HistoryController(IMemberHistoryService historyService)
        {
            _historyService = historyService;
        }


        /// <summary>
        /// Get all histories 
        /// </summary>
        /// <param name="memberId">Member Id</param>
        /// <param name="title">History title </param>
        /// <param name="startDate">Histories start</param>
        /// <param name="endDate">Histories End</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<MemberHistoryDto>> GetHistoriesAsync([FromQuery]string memberId = null, [FromQuery] string title = null, [FromQuery] DateTimeOffset? startDate =null, [FromQuery] DateTimeOffset? endDate = null)
        {

            return(await _historyService.GetHistoriesAsync(memberId, title, startDate, endDate))
                .Select( x => x.AsDto());

            //Logger.LogInfromation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {members}");
        }

        /// <summary>
        /// Get History  rBy Id
        /// </summary>
        /// <param name="id">History Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberHistoryDto>> GetHistoryById(string id)
        {
            var member = await _historyService.GetHistoryAsync(id);

            if (member == null) return NotFound(new ApiResponse(404));

            return member.AsDto();
        }

        /// <summary>
        /// Create a new history
        /// </summary>
        /// <param name="historyDto">History to create</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MemberHistoryDto>> CreateHistory([FromBody] MemberHistoryDto historyDto)
        {
            var historyToCreate = historyDto.FromDto();
            var createdHistory = await _historyService.CreateHistoryAsync(historyToCreate);
            if (createdHistory == null)
                return BadRequest("Failed to Add Member");


            return CreatedAtAction(nameof(GetHistoryById), new { id = createdHistory.Id }, createdHistory.AsDto());

        }

        /// <summary>
        /// Update a hisotry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="historyDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpadateHistory(string id, [FromBody] MemberHistoryDto historyDto)
        {
            if (historyDto.Id != id) return BadRequest("Failed to update");
            var historyToUpdate = await _historyService.GetHistoryAsync(id);
            if (historyToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedHistory = await _historyService.UpdateHistoryAsync(historyDto.FromDto());
            if (updatedHistory != null) return NoContent();

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a history
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            var result = await _historyService.DeleteHistoryAsync(id);
            if (result) return NoContent();

            return BadRequest("Problem deleting the message");
        }
    }
}
