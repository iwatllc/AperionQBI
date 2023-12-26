using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using AperionQB.Application.Features.QuickBooks.Commands;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitGetCustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitGetCustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitGetCustomers()
    {
        bool result = _mediator.Send(new CommandGetCustomers()).Result;
        if (result)
        {
            return Ok("Got all customers from Intuit");
        }
        else
        {
            return BadRequest("Unable to get customers from intuit. Please check logs and fix the issued");
        }
    }
}
