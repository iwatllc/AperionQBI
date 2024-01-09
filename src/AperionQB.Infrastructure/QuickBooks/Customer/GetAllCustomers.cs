using System.Collections.ObjectModel;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using AperionQB.Application.Interfaces;


namespace AperionQB.Infrastructure.QuickBooks
{
    public class GetAllCustomers : QuickBooksOperation
    {
        public GetAllCustomers(IApplicationDbContext context, IInfoHandler handler): base(context, handler){}
        public void getCustomersFromIntuit()
        {

            QueryService<Customer> querySvc = new QueryService<Customer>(base.serviceContext);
            double numCustomers = (double)querySvc.ExecuteIdsQueryForCount("SELECT count(*) FROM Customer");

            StreamWriter customerCSV = new StreamWriter("Customers.CSV");
            customerCSV.WriteLine("Intuit ID,Customer Name,Billing Address, Company Name, Name, Primary Email Address,");

            for (int i = 0; i < Math.Ceiling(numCustomers / 10); i++)
            {
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
                foreach (Customer customer in customers)
                {
                    customerCSV.WriteLine($"{customer.Id},{customer.CompanyName},\"{customer.BillAddr?.Line1} {customer.BillAddr?.City}\",{customer.DisplayName},\"{customer.FamilyName},{customer.GivenName}\", {customer.PrimaryEmailAddr?.Address},");
                }
            }
            customerCSV.Close();
        }
    }
}
