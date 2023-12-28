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
using AperionQB.Application.Features.QuickBooks.Commands;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GetNewTokensController : ControllerBase
{ 

    private readonly IMediator _mediator;

    public GetNewTokensController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public IActionResult GetNewTokens()
    {
        string url = _mediator.Send(new CommandGetKeys()).Result;
        return Redirect(url);
        
    }
}
