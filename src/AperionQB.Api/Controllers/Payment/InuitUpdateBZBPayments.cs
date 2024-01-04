using AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitUpdateBZBPayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitUpdateBZBPayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitUpdateBZBPayments()
    {
        try
        {
            _mediator.Send(new CommandFireUpdatePaymentsJob());
            return Ok("Successfully scheduled new payments to be created in QuickBooks");
        }
        catch (Exception e)
        {
            return BadRequest("Unable to schedule new payments to be created in QuickBooks");
        }

    }
}

