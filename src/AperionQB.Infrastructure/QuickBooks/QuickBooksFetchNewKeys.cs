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
	public class QuickBooksFetchNewKeys
	{
        public static OAuth2Client? Client { get; set; } = null;

       

        public static OAuth2Client Initialize()
        {
            return new("ABcbwY3tWCZC4BN2JTR7nWVhHCfSfTaVsXyGM70BJxtsMDrPfW", "wxoJrSl5KWJbe6DVnHndSMeWE8nRRBZ3K4LvmsPQ", "https://localhost:7116/api/RefreshTokenCallback", "sandbox");
        }

        public static string GetAuthorizationURL(OAuth2Client client, params OidcScopes[] scopes)
        {
            
 
            if (Client == null)
            {
                Initialize();
            }

            #pragma warning disable CS8602
            Client = client;
            string result = client.GetAuthorizationURL(scopes.ToList());
            return result;
        }



    

        public static async Task GetAuthTokensAsync(string code, string realmId)
        {
            TokenResponse response = await Client.GetBearerTokenAsync(code);
            var access_token = response.AccessToken;
            var refresh_token = response.RefreshToken;
            writeTokensAsJson(access_token, refresh_token, realmId);

        }

        public static bool writeTokensAsJson(string access_token, string refresh_token, string realmId)
        {
            string json = File.ReadAllText("/Users/taylorfernandez/Desktop/aperion-quickbooks-integration/src/AperionQB.Infrastructure/QuickBooks/Tokens.json");
            Tokens tokens = JsonConvert.DeserializeObject<Tokens>(json);
            tokens.AccessToken = access_token;
            tokens.RefreshToken = refresh_token;
            tokens.RealmId = realmId;
            string backtojson = JsonConvert.SerializeObject(tokens);
            File.WriteAllText("/Users/taylorfernandez/Desktop/aperion-quickbooks-integration/src/AperionQB.Infrastructure/QuickBooks/Tokens.json", backtojson);


            return false;
        }
	}
}

