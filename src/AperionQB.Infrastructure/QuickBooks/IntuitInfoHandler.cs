using System;
using AperionQB.Domain.Entities.QuickBooks;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
	public class IntuitInfoHandler
	{
		public IntuitInfoHandler()
		{
		}

        public static bool UpdateTokens(string access_token, string refresh_token, string realmId)
        {
            IntuitInfo info = getIntuitInfo();
            info.AccessToken = access_token;
            info.RefreshToken = refresh_token;
            info.RealmId = realmId;
            saveIntuitInfo(info);

            return false;
        }

        public static IntuitInfo getIntuitInfo()
        {
            try
            {
                string json = File.ReadAllText("/Users/taylorfernandez/Desktop/aperion-quickbooks-integration/src/AperionQB.Infrastructure/QuickBooks/IntuitInfo.json");
                return JsonConvert.DeserializeObject<IntuitInfo>(json);
            }catch (Exception e)
            {
                Console.WriteLine("Error while loading IntuitInfo: Path to json file seems to be incorrect.");
                return null;
            }

        }

        public static bool saveIntuitInfo(IntuitInfo info)
        {
            try
            {
                string json = JsonConvert.SerializeObject(info);
                File.WriteAllText("/Users/taylorfernandez/Desktop/aperion-quickbooks-integration/src/AperionQB.Infrastructure/QuickBooks/IntuitInfo.json", json);
                return true;
            }catch (Exception e)
            {
                Console.WriteLine("Error while saving IntuitInfo: Path to json file seems to be incorrect.");
                return false;
            }

        }
    }
}

