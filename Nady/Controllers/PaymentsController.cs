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
    /// Payments Controllers
    /// </summary>
    public class PaymentsController : BaseApiController
    {
        private readonly IMemberPaymentService _paymentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paymentService"></param>
        public PaymentsController(IMemberPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        /// <summary>
        /// Get all Payments
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="name"></param>
        /// <param name="isScheduled"></param>
        /// <param name="paymentType"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="paymentTotal"></param>
        /// <param name="taxPercentage"></param>
        /// <param name="discountPercentage"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<MemberPaymentDto>> GetPaymentsAsync([FromQuery]string memberId = null,
            [FromQuery] string name = null,
            [FromQuery] bool? isScheduled = null,
            [FromQuery] int? paymentType = null,
            [FromQuery] decimal? paymentAmount = null,
            [FromQuery] decimal? paymentTotal = null,
            [FromQuery] double? taxPercentage = null,
            [FromQuery] double? discountPercentage = null,
            [FromQuery] DateTimeOffset? startDate = null,
            [FromQuery] DateTimeOffset? endDate = null)
        {

            return(await _paymentService.GetPaymentsAsync(memberId, name, isScheduled, paymentType, paymentAmount, paymentTotal, taxPercentage, discountPercentage, startDate, endDate))
                .Select( x => x.AsDto());

            //Logger.LogInfromation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {members}");
        }

        /// <summary>
        /// Get Payment By Id
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MemberPaymentDto>> GetPaymentById(string id)
        {
            var payment = await _paymentService.GetPaymentAsync(id);

            if (payment == null) return NotFound(new ApiResponse(404));

            return payment.AsDto();
        }

        /// <summary>
        /// Create a new Payment
        /// </summary>
        /// <param name="paymentDto">Payment to create</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MemberPaymentDto>> CreatePayment([FromBody] MemberPaymentDto paymentDto)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(paymentDto.FromDto());
            if (createdPayment == null)
                return BadRequest("Failed to Add Payment");


            return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.Id }, createdPayment.AsDto());

        }

        /// <summary>
        /// Update a payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpadateHistory(string id, [FromBody] MemberPaymentDto paymentDto)
        {
            if (paymentDto.Id != id) return BadRequest("Failed to update");
            var paymentToUpdate = await _paymentService.GetPaymentAsync(id);
            if (paymentToUpdate == null) return NotFound(new ApiResponse(404));
            var updatedPayment = await _paymentService.UpdatePaymentAsync(paymentDto.FromDto());
            if (updatedPayment != null) return NoContent();

            return BadRequest("Failed to update");
        }

        /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePayment(string id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (result) return NoContent();

            return BadRequest("Problem deleting the Payment");
        }
    }
}
