using System;
namespace AperionQB.Domain.Entities.QuickBooks
{
    [Serializable]
    public class Tokens 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public object AccessToken { get; set; }
        public object RefreshToken { get; set; }
        public object RealmId { get; set; }
        public object Env { get; set; }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public Tokens Tokens { get; set; }
        }

    }
}
