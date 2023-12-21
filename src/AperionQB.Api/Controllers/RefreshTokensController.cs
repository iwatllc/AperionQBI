using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Reflection;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenController : ControllerBase
{ 

    private readonly IMediator _mediator;

    public RefreshTokenController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public IActionResult RefreshToken()
    {
        OAuth2Client Client =  QuickBooksFetchNewKeys.Initialize();
        string authorizeUrl = QuickBooksFetchNewKeys.GetAuthorizationURL(Client, OidcScopes.Accounting);
        Console.WriteLine(authorizeUrl);
        return Redirect(authorizeUrl);


    }


}
