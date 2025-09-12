using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Api.Dtos.Requests;
using Payments.Api.Dtos.Response;
using Payments.Application.Services.Interfaces;
using Payments.Domain.Entities;
using System.Net;

namespace Payments.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<PaymentResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Payment")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _paymentService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PaymentResponse>>(result));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaymentResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Payment")]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _paymentService.GetByIdAsync(id);
            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<PaymentResponse>(result));
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Payment")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PaymentRequest request, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Payment>(request);
            await _paymentService.AddAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, null);
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Payment")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] PaymentRequest request, CancellationToken cancellationToken = default)
        {
            var game = _mapper.Map<Payment>(request);
            game.Id = id;
            await _paymentService.UpdateAsync(game);
            return NoContent();
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Payment")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            await _paymentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
