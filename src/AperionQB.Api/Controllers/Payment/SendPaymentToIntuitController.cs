using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        //TODO remove params from method; change to get all info from table in db
        [HttpGet]
        public async Task<IActionResult> SendPaymentToIntuit([FromQuery] int totalAmount, string lineDesc, int customerID, string privateNote)
        {
            int result =  _mediator.Send(new AddPaymentCommand(totalAmount, lineDesc, customerID, privateNote)).Result;

            if (result != -1)
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

