using System;
namespace AperionQB.Domain.Entities.QuickBooks
{
    [Serializable]
    public class IntuitInfo 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public object AccessToken { get; set; }
        public object RefreshToken { get; set; }

        //Also referred to as companyID
        public object RealmId { get; set; }
        public object Env { get; set; }

        public class Root
        {
            public IntuitInfo IntuitInfo { get; set; }
        }

    }
}
