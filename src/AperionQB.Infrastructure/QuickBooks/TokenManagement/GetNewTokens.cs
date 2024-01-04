using System;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;

namespace AperionQB.Infrastructure.QuickBooks.TokenManagement
{
	public class GetNewTokens
	{
        public async Task GetAuthTokensAsync(string code, string realmId)
        {
            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
            OAuth2Client client = new OAuth2Client(info.ClientId, info.ClientSecret, info.RedirectUrl, (string)info.Env);
            TokenResponse response = await client.GetBearerTokenAsync(code);
            var access_token = response.AccessToken;
            var refresh_token = response.RefreshToken;
            IntuitInfoHandler.UpdateTokens(access_token, refresh_token, realmId);

        }
    }
}

