﻿using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenCallbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public RefreshTokenCallbackController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult> RefreshTokenCallback()
    {
     
        return Ok("Fetched New Keys");

    }
}
