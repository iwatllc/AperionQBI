using System;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.Data;
using Newtonsoft.Json;
using Quartz;

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
            IntuitInfo info = null;
            try
            {
                string json = File.ReadAllText(getIntuitInfoPath());
                info =  JsonConvert.DeserializeObject<IntuitInfo>(json);
                if(info.AccessToken != null && info.RefreshToken != null)
                {
                    return info;
                }
                else
                {
                    throw new ArgumentException();
                 
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error while loading IntuitInfo: Path to json file seems to be incorrect.");
                return null;
            }
            catch (ArgumentException)
            {
          
                Console.WriteLine("It appears that tokens do not currently exist. Get new tokens by going to {base url}/api/GetNewTokens and try again");
                return info;
            }
        }

        public static bool saveIntuitInfo(IntuitInfo info)
        {
            try
            {
                string json = JsonConvert.SerializeObject(info);
             
                File.WriteAllText(getIntuitInfoPath(), json);
                return true;
            }catch (Exception e)
            {
                Console.WriteLine("Error while saving IntuitInfo: Path to json file seems to be incorrect.");
                return false;
            }

        }
       
        public static string getIntuitInfoPath()
        {
            try
            {
                string temp = Directory.GetCurrentDirectory();
                temp = temp.Replace("AperionQB.Api", "AperionQB.Infrastructure/QuickBooks/IntuitInfo.json");
                Console.WriteLine(temp);
                return temp;
            }catch (Exception e)
            {
                Console.WriteLine("Path could not automatically be set. Please update path in IntuitInfoHandler.cs");
                return "";
            }
        }
    }
}

