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
public class RefreshAccessToken : ControllerBase
{
    private readonly IMediator _mediator;

    public RefreshAccessToken(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> RefreshAccessTokens()
    {
        bool response = _mediator.Send(new CommandRefreshAccessTokens()).Result;
        if (response)
        {
            return Ok("Successfully refreshed access tokens!");
        }
        else
        {
            return BadRequest("An error occured. Check logs and try again");
        }

    }
}
