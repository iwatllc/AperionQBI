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
        OAuth2Client Client;

        public OAuth2Client Initialize()
        {
            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
            return new(info.ClientId, info.ClientSecret, info.RedirectUrl, (string)info.Env);
        }

        public void setClient(OAuth2Client client)
        {
            Client = client;
        }

        public string GetAuthorizationURL(params OidcScopes[] scopes)
        {
            
 
            if (Client == null)
            {
                Initialize();
            }

            #pragma warning disable CS8602
            string result = Client.GetAuthorizationURL(scopes.ToList());
            return result;
        }


        public async Task<bool> refreshAccessTokens()
        {

            Client = this.Initialize();
            try
            {
                IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
                TokenResponse response = await Client.RefreshTokenAsync((string)info.RefreshToken);
                IntuitInfoHandler.UpdateTokens(response.AccessToken, response.RefreshToken, (string)info.RealmId);
            }catch (Exception e)
            {
                Console.WriteLine(DateTime.Now +": " +  e.Message);
                return false;
            }

            return true;
        }
	}
}

