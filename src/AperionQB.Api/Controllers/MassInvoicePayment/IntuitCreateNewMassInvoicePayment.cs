using AperionQB.Application.Features.QuickBooks.Commands.MassInvoicePayment;
using AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitCreateNewMassInvoicePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitCreateNewMassInvoicePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> intuitCreateNewMassInvoicePayment()
    {
        Console.WriteLine("HERE");
        try
        {
            await _mediator.Send(new CommandFireCreateMassInvoicePaymentJob());
            return Ok("Successfully scheduled new payments to be created in QuickBooks");
        }
        catch (Exception e)
        {
            return BadRequest("Unable to schedule new payments to be created in QuickBooks: " + e.Message);
        }

    }
}