using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Diagnostics;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenController : ControllerBase
{ 

    private readonly IMediator _mediator;

    public RefreshTokenController(IMediator mediator)
    {
        _mediator = mediator;
        Serilog.Log.Logger = new Serilog.LoggerConfiguration().CreateLogger();
    }


    [HttpGet]
    public async Task<ActionResult> RefreshToken()
    {
        OAuth2Client Client =  QuickBooksFetchNewKeys.Initialize();
        Redirect(QuickBooksFetchNewKeys.GetAuthorizationURL(Client, OidcScopes.Accounting));

        return Ok("Fetched New Keys");

    }


}
