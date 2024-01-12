using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using AperionQB.Application.Features.QuickBooks.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GetPaymentMethods : ControllerBase
{
    private readonly IMediator _mediator;

    public GetPaymentMethods(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> getPaymentMethods()
    {
        string json = _mediator.Send(new CommandGetPaymentMethods()).Result;
        if (json != "false")
        {
            return Ok(json);
        }
        else
        {
            return BadRequest("An error occured while getting payment methods.");
        }
    }

}
