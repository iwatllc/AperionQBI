using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntuitAddCustomer : ControllerBase
    {

        private readonly IMediator _mediator;

        public IntuitAddCustomer(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> addCustomerToIntuit([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string primaryEmailAddress, [FromQuery] string BillAddrL1, [FromQuery] string City, [FromQuery] string displayName)
        {
            bool result = _mediator.Send(new CommandAddCustomer(firstName, lastName, primaryEmailAddress, BillAddrL1, City, displayName)).Result;
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

