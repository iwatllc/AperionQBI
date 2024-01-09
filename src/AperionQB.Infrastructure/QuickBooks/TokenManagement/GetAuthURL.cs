using System;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Options;


namespace AperionQB.Infrastructure.QuickBooks.TokenManagement
{
    public class GetAuthURL

    {
        QuickBooksKeyActions actions;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;

        public GetAuthURL(IApplicationDbContext context, IInfoHandler handler)
        {
            _context = context;
            _handler = handler; 
            actions = new QuickBooksKeyActions(_context, _handler);
        }


        public string getAuthURL()
		{
            OAuth2Client Client = actions.Initialize();
            actions.setClient(Client);
            string authorizeUrl = actions.GetAuthorizationURL(OidcScopes.Accounting);
            return authorizeUrl;
        }
	}
}

