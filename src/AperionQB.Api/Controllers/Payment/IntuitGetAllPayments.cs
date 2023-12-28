using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Payment;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitGetAllPaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitGetAllPaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitGetAllPayments()
    {
        bool result = await _mediator.Send(new GetAllPaymentsCommand());
        if (result)
        {
            return Ok("Got all Payments from Intuit");
        }
        else
        {
            return BadRequest("Unable to get customers from intuit. Please check logs and fix the issue");
        }
    }
}
