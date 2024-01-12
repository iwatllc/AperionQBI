using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.PaymentMethod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GetPaymentMethod : ControllerBase
{
    private readonly IMediator _mediator;

    public GetPaymentMethod(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> getPaymentMethod([FromQuery]string id)
    {
        string json = _mediator.Send(new CommandGetPaymentMethod(Int32.Parse(id))).Result;
        if (json != "false")
        {
            return Ok(json);
        }
        else
        {
            return BadRequest("An error occured while getting payment method with ID " + id + ".");
        }
    }

}
