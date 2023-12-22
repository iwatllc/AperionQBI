using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.Json;
using System.Web;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
	public class QuickBooksKeyActions
	{
        public static OAuth2Client? Client { get; set; } = null;



        public static OAuth2Client Initialize()
        {
            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
            return new(info.ClientId, info.ClientSecret, info.RedirectUrl, (string)info.Env);
        }

        public static void setClient(OAuth2Client client)
        {
            Client = client; 
        }

        public static string GetAuthorizationURL(params OidcScopes[] scopes)
        {
            
 
            if (Client == null)
            {
                Initialize();
            }

            #pragma warning disable CS8602
            string result = Client.GetAuthorizationURL(scopes.ToList());
            return result;
        }

        public static async Task GetAuthTokensAsync(string code, string realmId)
        {

            TokenResponse response = await Client.GetBearerTokenAsync(code);
            var access_token = response.AccessToken;
            var refresh_token = response.RefreshToken;
            IntuitInfoHandler.UpdateTokens(access_token, refresh_token, realmId);

        }

        public static async Task refreshAccessTokens()
        {
            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
            TokenResponse response = await Client.RefreshTokenAsync((string)info.RefreshToken);
            IntuitInfoHandler.UpdateTokens(response.AccessToken, response.RefreshToken, (string)info.RealmId);
            
        }
	}
}

