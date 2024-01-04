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
        public async Task<IActionResult> addCustomerToIntuit()
        {
            bool result = _mediator.Send(new CommandAddCustomer()).Result;

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