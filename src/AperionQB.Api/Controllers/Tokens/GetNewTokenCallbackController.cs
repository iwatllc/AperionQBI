﻿using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using AperionQB.Application.Features.QuickBooks.Commands;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GetNewTokensCallbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetNewTokensCallbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetNewTokensCallback([FromQuery] string code, [FromQuery] string state, [FromQuery] string realmId)
    {
        try
        {
            await _mediator.Send(new CommandGetKeysCallback(code, state,realmId));
        }catch (Exception e)
        {
            return BadRequest("Error has occured: " + e.Message + "\n" + e.ToString());
        }
        
        return Ok("Successfully fetched New Keys");

    }
}
