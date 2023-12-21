using System;
using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;

namespace AperionQB.Infrastructure.QuickBooks
{
	public class QuickBooksFetchNewKeys
	{
		public static QuickBooksAuthTokens? Tokens { get; set; } = null;
		public static OAuth2Client? Client { get; set; } = null;
        public static string path = "/Users/taylorfernandez/Desktop/aperion-quickbooks-integration/src/AperionQB.Infrastructure/IntuitAPIkeys.json";

        public static OAuth2Client Initialize()
        {
            return new("ABcbwY3tWCZC4BN2JTR7nWVhHCfSfTaVsXyGM70BJxtsMDrPfW", "WYW2Gb4M21WyliC04dzqaJKz1uOnoN1i7UKmhWPW", "https://developer.intuit.com/v2/OAuth2Playground/RedirectUrl", "sandbox");
        }

        public static string GetAuthorizationURL(OAuth2Client Client, params OidcScopes[] scopes)
        {
 
            if (Client == null ||Tokens == null)
            {
                Initialize();
            }

            #pragma warning disable CS8602

    
            return Client.GetAuthorizationURL(scopes.ToList());
        }



        public static bool CheckQueryParamsAndSet(string queryString, bool suppressErrors = true)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(queryString);

            
            if (query["code"] != null && query["realmId"] != null)
            {


                TokenResponse responce = Client.GetBearerTokenAsync(query["code"]).Result;

                Tokens.AccessToken = responce.AccessToken;
                Tokens.RefreshToken = responce.RefreshToken;
                Tokens.RealmId = query["realmId"];
                throw new Exception("Here");
       
            }
            else
            {
                if (suppressErrors)
                {
                    return false;
                }
                else
                {
                    throw new InvalidDataException(
                        $"The 'code' or 'realmId' was not present in the query parameters '{query}'."
                    );
                }
            }
        }

        public static void WriteTokensAsJson(QuickBooksAuthTokens authTokens)
        {

            string serialized = JsonSerializer.Serialize(authTokens, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });

            File.WriteAllText(path, serialized);
        }
	}
}

