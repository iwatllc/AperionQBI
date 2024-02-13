using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using AperionQB.Application.Features.QuickBooks.Commands;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;


namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class SaveCustomersToDatabase : QuickBooksOperation
    { 
        public SaveCustomersToDatabase(IApplicationDbContext context, IInfoHandler handler): base(context, handler)
        {
        }


        public async Task<string> saveCustomersToDatabase()
        {
            Console.WriteLine("Here");
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
                    //if the customer doesnt exist in the table, add it
                    if (_context.QBCustomers.Where((cstmr) => cstmr.QBCustomerID == Int32.Parse(customer.Id)).FirstOrDefault() == null)
                    {
                        Console.WriteLine(customer.DisplayName + ": True");
                        QBCustomers c = new QBCustomers();
                        c.QBCustomerName = customer.DisplayName;
                        c.QBCustomerID = Int32.Parse(customer.Id);
                        _context.QBCustomers.Add(c);
                        Console.WriteLine("Saving");
                        await _context.SaveChangesAsync();
                        customerCSV.WriteLine(
                            $"{customer.Id},{customer.ContactName},\"{customer.BillAddr?.Line1} {customer.BillAddr?.City}\",{customer.DisplayName},\"{customer.FamilyName},{customer.GivenName}\", {customer.PrimaryEmailAddr?.Address},");
                    }
                }
                customerCSV.Close();
            }

            return "success";
        }
    }
}