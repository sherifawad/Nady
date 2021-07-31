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
    /// Scheduled Payments Controllers
    /// </summary>
    public class ScheduledPaymentsController : BaseApiController
    {
        private readonly IScheduledPaymentService _scheduledPaymentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheduledPaymentService"></param>
        public ScheduledPaymentsController(IScheduledPaymentService scheduledPaymentService)
        {
            _scheduledPaymentService = scheduledPaymentService;
        }

        /// <summary>
        /// Get Scheduled Payments
        /// </summary>
        /// <param name="memberPaymentId"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="fulfiled"></param>
        /// <param name="paymentMethod"></param>
        /// <param name="note"></param>
        /// <param name="paymentDueStartDate"></param>
        /// <param name="paymentDueEndtDate"></param>
        /// <param name="fulfiledStartDate"></param>
        /// <param name="fulfiledEndtDate"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ScheduledPaymentDto>> GetScheduledPaymentsAsync(
            [FromQuery]string memberPaymentId = null,
            [FromQuery]decimal? paymentAmount = null,
            [FromQuery]bool? fulfiled = null,
            [FromQuery]int? paymentMethod = null,
            [FromQuery]string note = null,
            [FromQuery]DateTimeOffset? paymentDueStartDate = null,
            [FromQuery]DateTimeOffset? paymentDueEndtDate = null,
            [FromQuery]DateTimeOffset? fulfiledStartDate = null,
            [FromQuery] DateTimeOffset? fulfiledEndtDate = null)
        {

            return(await _scheduledPaymentService.GetScheduledPaymentsAsync(memberPaymentId, paymentAmount, fulfiled, paymentMethod, note, paymentDueStartDate, paymentDueEndtDate, fulfiledStartDate, fulfiledEndtDate))
                .Select( x => x.AsDto());

            //Logger.LogInfromation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {members}");
        }

        /// <summary>
        /// Get Scheduled Payment By Id
        /// </summary>
        /// <param name="id">Scheduled Payment Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduledPaymentDto>> GetScheduledPaymentById(string id)
        {
            var scheduledPayment = await _scheduledPaymentService.GetScheduledPaymentAsync(id);

            if (scheduledPayment == null) return NotFound(new ApiResponse(404));

            return scheduledPayment.AsDto();
        }

        /// <summary>
        /// Create a new Scheduled Payment
        /// </summary>
        /// <param name="scheduledPaymentDto">Scheduled Payment to create</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduledPaymentDto>> CreateScheduledPayment([FromBody] ScheduledPaymentDto scheduledPaymentDto)
        {
            var createdScheduledPayment = await _scheduledPaymentService.CreateScheduledPaymentAsync(scheduledPaymentDto.FromDto());
            if (createdScheduledPayment == null)
                return BadRequest("Failed to Add Payment");


            return CreatedAtAction(nameof(GetScheduledPaymentById), new { id = createdScheduledPayment.Id }, createdScheduledPayment.AsDto());

        }

        /// <summary>
        /// Update a Scheduled payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheduledPaymentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpadateHistory(string id, [FromBody] ScheduledPaymentDto scheduledPaymentDto)
        {
            if (scheduledPaymentDto.Id != id) return BadRequest("Failed to update");
            var scheduledPaymentToUpdate = await _scheduledPaymentService.GetScheduledPaymentAsync(id);
            if (scheduledPaymentToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedScheduledPayment = await _scheduledPaymentService.UpdateScheduledPaymentAsync(scheduledPaymentDto.FromDto());
            if (updatedScheduledPayment != null) return NoContent();

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a Scheduled payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePayment(string id)
        {
            var result = await _scheduledPaymentService.DeleteScheduledPaymentAsync(id);
            if (result) return NoContent();

            return BadRequest("Problem deleting the Scheduled Payment");
        }
    }
}
