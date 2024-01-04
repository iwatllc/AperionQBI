using AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitCreateNewBZBPayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitCreateNewBZBPayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitCreateNewBZBPayments()
    {
        Console.WriteLine("HERE");
        try
        {
            await _mediator.Send(new CommandFireNewPaymentsJob());
            return Ok("Successfully scheduled new payments to be created in QuickBooks");
        }
        catch (Exception e)
        {
            return BadRequest("Unable to schedule new payments to be created in QuickBooks");
        }

    }
}