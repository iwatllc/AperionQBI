using System.Collections.ObjectModel;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using AperionQB.Application.Interfaces;


namespace AperionQB.Infrastructure.QuickBooks
{
    public class GetAllCustomers : QuickBooksOperation
    {
        public GetAllCustomers(IApplicationDbContext context, IInfoHandler handler): base(context, handler){}
        public string[][] getCustomersFromIntuit()
        {

            QueryService<Customer> querySvc = new QueryService<Customer>(base.serviceContext);
            double numCustomers = (double)querySvc.ExecuteIdsQueryForCount("SELECT count(*) FROM Customer");

            StreamWriter customerCSV = new StreamWriter("Customers.CSV");
            customerCSV.WriteLine("Intuit ID,Customer Name,Billing Address, Company Name, Name, Primary Email Address,");

            List<List<String>> result = new List<List<string>>();
            for (int i = 0; i < Math.Ceiling(numCustomers / 1000); i++)
            {
                ReadOnlyCollection<Customer> companyInfo;
                if (i == 0)
                {
                    companyInfo = querySvc.ExecuteIdsQuery($"SELECT * FROM Customer MAXRESULTS 1000");
                }
                else
                {
                    companyInfo = querySvc.ExecuteIdsQuery($"SELECT * FROM Customer STARTPOSITION {(1000 * i) + 1} MAXRESULTS 1000");
                }

                Customer[] customers = companyInfo.ToArray<Customer>();
                foreach (Customer customer in customers)
                {
                    List<String> entry = new List<String>();
                    entry.Add(customer.GivenName);
                    entry.Add(customer.Id);
                    result.Add(entry);
                    customerCSV.WriteLine($"{customer.Id},{customer.CompanyName},\"{customer.BillAddr?.Line1} {customer.BillAddr?.City}\",{customer.DisplayName},\"{customer.FamilyName},{customer.GivenName}\", {customer.PrimaryEmailAddr?.Address},");
                }
            }
            customerCSV.Close();
            Console.WriteLine("Array Size: " + result.Count());
            String[][] resArray = new String[result.Count][];
            int j = 0;
            foreach(List<String> list in result)
            {
                resArray[j] = list.ToArray<String>();
                j++;
            }
            Console.WriteLine("String arr size: " + resArray.Length);
            return resArray; ;
        }
    }
}
