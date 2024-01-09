using System;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;

namespace AperionQB.Infrastructure.QuickBooks.TokenManagement
{
	public class GetNewTokens
	{
        private readonly IInfoHandler _handler;
        public GetNewTokens(IInfoHandler _handler)
        {
            this._handler = _handler;
        }

        public async Task GetAuthTokensAsync(string code, string realmId)
        {
            IntuitInfo info = _handler.getIntuitInfo();
            OAuth2Client client = new OAuth2Client(info.ClientId, info.ClientSecret, info.RedirectUrl, (string)info.Env);
            TokenResponse response = await client.GetBearerTokenAsync(code);
            var access_token = response.AccessToken;
            var refresh_token = response.RefreshToken;
            _handler.UpdateTokens(access_token, refresh_token, realmId);

        }
    }
}

