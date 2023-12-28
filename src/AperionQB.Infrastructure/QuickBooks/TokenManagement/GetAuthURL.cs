using System;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Options;

namespace AperionQB.Infrastructure.QuickBooks.TokenManagement
{
    public class GetAuthURL : QuickBooksOperation

    {
        QuickBooksKeyActions actions;
        public GetAuthURL()
        {
            actions = new QuickBooksKeyActions();
        }


        public string getAuthURL()
		{
            OAuth2Client Client = actions.Initialize();
            actions.setClient(Client);

            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();

            string authorizeUrl = actions.GetAuthorizationURL(OidcScopes.Accounting);
            return authorizeUrl;
        }
	}
}

