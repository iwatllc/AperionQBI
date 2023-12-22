using System;
using AperionQB.Domain.Entities.QuickBooks;
using AperionQB.Infrastructure.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendNewInvoicesToIntuitController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SendNewInvoicesToIntuitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public IActionResult SendInvoicesToIntuit()
        {
            OAuth2Client Client = QuickBooksKeyActions.Initialize();
            QuickBooksKeyActions.setClient(Client);

            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();

            string authorizeUrl = QuickBooksKeyActions.GetAuthorizationURL(OidcScopes.Accounting);
            return Redirect(authorizeUrl);

        }
    }
}

