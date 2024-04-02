using AperionQB.Application.Features.QuickBooks.Commands.QuartsJobs;
using AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitDeleteBZBPayments : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitDeleteBZBPayments(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitUpdateBZBPayment()
    {
        try
        {
            _mediator.Send(new CommandFireDeletePaymentsJob());
            return Ok("Successfully scheduled new payments to be created in QuickBooks");
        }
        catch (Exception e)
        {
            return BadRequest("Unable to schedule new payments to be created in QuickBooks");
        }

    }
}