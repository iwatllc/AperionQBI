using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities;
using AperionQB.Domain.Entities.QuickBooks;
using AperionQB.Infrastructure.Logging;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
    public class IntuitInfoHandler : IInfoHandler
    {
        private  Logger logger;
        private readonly IApplicationDbContext _context;
        public IntuitInfoHandler(IApplicationDbContext _context)
        {
            this._context = _context;
            logger = new Logger();
        }

        public void ensureEntryInDB()
        {
            try
            {
                int temp =  _context.Configitems.ToList().Where((conf) => (conf.Code == "intuit_info")).Count();
                if(temp == 0)
                {
                    IntuitInfo info = new IntuitInfo();
                    info.RealmId = "4620816365375193410";
                    Configitem item = new Configitem();
                    item.Code = "intuit_info";
                    item.Value = JsonConvert.SerializeObject(info);
                    _context.Configitems.Add(item);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": An error occured while checking if intuit info is in db: " + e.Message);
            }
        }

        public  bool UpdateTokens(string access_token, string refresh_token, string realmId)
        {
            Console.WriteLine("getting info");
            IntuitInfo info = getIntuitInfo();
    
            info.AccessToken = access_token;
            info.RefreshToken = refresh_token;
            info.RealmId = realmId;
            Console.WriteLine("Saving info");
            saveIntuitInfo(info);

            return false;
        }

        public  bool updateIntuitInfo(string clientID, string clientSecret, string callbackURL)
        {
            IntuitInfo info;
            try
            {
                Console.WriteLine("getting info");
                info = getIntuitInfo();
                if(clientID != null & clientID != "") { info.ClientId = clientID; }
                if (clientSecret != null && clientSecret != "") { info.ClientSecret = clientSecret; }
                if(callbackURL != null && callbackURL != ""){ info.RedirectUrl = callbackURL;}
                Console.WriteLine("Saving info");
                saveIntuitInfo(info);
                return true;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": An error occured while create intuit info object: " + e.Message);
                return false; 
            }

        }

        public IntuitInfo getIntuitInfo()
        {
            IntuitInfo info = null;
            string json;
            try
            {

                json = _context.Configitems.ToList().Where((conf) => (conf.Code == "intuit_info")).First().Value;

                info = JsonConvert.DeserializeObject<IntuitInfo>(json);
                if (info.AccessToken != null && info.RefreshToken != null)
                {
                    return info;
                } else
                {

                    Console.WriteLine("It appears that tokens do not currently exist. Get new tokens by going to {base url}/api/GetNewTokens and try again");
                    return info;
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error while loading IntuitInfo: Path to json file seems to be incorrect.");
                return null;
            }
        }

        public  bool saveIntuitInfo(IntuitInfo info)
        {
            Configitem item;
            try {
                string json = JsonConvert.SerializeObject(info);
            
                item = _context.Configitems.ToList().Where((conf) => (conf.Code == "intuit_info")).First();
            
                item.Value = json;
              
                _context.Configitems.Update(item);
                _context.SaveChanges();
           
                return true;
            } catch (Exception e){
                Console.WriteLine("Error while saving IntuitInfo: " + e.Message);
                return false;
            }
        }

        public  string getIntuitInfoPath()
        {
            try
            {
                string temp = Directory.GetCurrentDirectory();
                temp = temp.Replace("AperionQB.Api", "AperionQB.Infrastructure/QuickBooks/IntuitInfo.json");
                return temp;
            }
            catch (Exception)
            {
                Console.WriteLine("Path could not automatically be set. Please update path in IntuitInfoHandler.cs");
                return "";
            }
        }
    }
}