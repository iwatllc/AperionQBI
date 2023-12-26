using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendPaymentToIntuitController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SendPaymentToIntuitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //Not Final. Just used for testing
        [HttpGet]
        public async Task<IActionResult> SendPaymentToIntuit([FromQuery] int TotalAmt, [FromQuery] string ReferenceType, [FromQuery] int customerID, [FromQuery]string memo)
        {
            bool result = _mediator.Send(new AddPaymentCommand(TotalAmt, ReferenceType, customerID, memo)).Result;
            if (result)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Bad");
            }
            
        }
    }
}

