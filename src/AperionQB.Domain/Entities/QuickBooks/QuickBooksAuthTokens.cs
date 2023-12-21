using System;
namespace AperionQB.Domain.Entities.QuickBooks
{
	public class QuickBooksAuthTokens
	{
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? RealmId { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string RedirectUrl { get; set; } = "https://developer.intuit.com/v2/OAuth2Playground/RedirectUrl";
        public string Environment { get; set; } = "sandbox";
        
    }
}

