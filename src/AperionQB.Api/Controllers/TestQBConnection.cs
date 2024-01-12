using AperionQB.Application.Features.Invoices.Queries;
using AperionQB.Application.Features.QuickBooks.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestQBConnection : ControllerBase
{
    private readonly IMediator _mediator;

    public TestQBConnection(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<bool>> TestQbiStatus([FromQuery]string id)
    {
        var result = _mediator.Send(new CommandTestQbConnection(Int32.Parse(id))).Result;
        if (result)
        {
            return Ok("Successfully connected to QB");
        }
        else
        {
            return BadRequest("Could not connect to QB");
        }
    }

}
