using System;
using System.Collections.ObjectModel;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
    public class GetAllCustomers
    {
        public static void getCustomersFromIntuit()
        {

            IntuitInfo info = IntuitInfoHandler.getIntuitInfo();
            string realmId = (string)info.RealmId;

            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator((string)info.AccessToken);
            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "23";

            // Create a QuickBooks QueryService using ServiceContext
            QueryService<Customer> querySvc = new QueryService<Customer>(serviceContext);
            double numCustomers = (double)querySvc.ExecuteIdsQueryForCount("SELECT count(*) FROM Customer");

            StreamWriter customerCSV = new StreamWriter("Customers.CSV");
            customerCSV.WriteLine("Intuit ID,Customer Name,Billing Address, Company Name, Name, Primary Email Address,");

            for(int i = 0; i < Math.Ceiling(numCustomers/10); i++) {
                ReadOnlyCollection<Customer> companyInfo;
                if (i == 0)
                {
                    companyInfo = querySvc.ExecuteIdsQuery($"SELECT * FROM Customer MAXRESULTS 10");

                }
                else
                {
                    companyInfo = querySvc.ExecuteIdsQuery($"SELECT * FROM Customer STARTPOSITION {(10 * i) + 1} MAXRESULTS 10");
                }


                Customer[] customers = companyInfo.ToArray<Customer>();
                Console.WriteLine(customers.Length);
                foreach (Customer customer in customers)
                {
                    customerCSV.WriteLine($"{customer.Id},{customer.CompanyName},\"{customer.BillAddr?.Line1} {customer.BillAddr?.City}\",{customer.DisplayName},\"{customer.FamilyName},{customer.GivenName}\", {customer.PrimaryEmailAddr?.Address},");
                }
            }

            customerCSV.Close();
            
        }
    }
}



