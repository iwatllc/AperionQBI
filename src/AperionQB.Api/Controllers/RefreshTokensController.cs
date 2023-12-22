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
using AperionQB.Domain.Entities.QuickBooks;

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
        OAuth2Client Client = QuickBooksKeyActions.Initialize();
        QuickBooksKeyActions.setClient(Client);

        IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
       
        string authorizeUrl = QuickBooksKeyActions.GetAuthorizationURL(OidcScopes.Accounting);
        return Redirect(authorizeUrl);
        
    }
}
