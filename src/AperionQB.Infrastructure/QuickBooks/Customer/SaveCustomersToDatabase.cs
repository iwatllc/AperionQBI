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
                    QBCustomers? temp= _context.QBCustomers.Where((cstmr) => cstmr.QBCustomerID == Int32.Parse(customer.Id)).FirstOrDefault();
                    //if the customer doesnt exist in the table, add it
                    if (temp == null)
                    {
                        Console.WriteLine(customer.DisplayName + ": True");
                        QBCustomers c = new QBCustomers();
                        c.QBCustomerName = customer.DisplayName;
                        c.QBCustomerID = Int32.Parse(customer.Id);
                        _context.QBCustomers.Add(c);
                        Console.WriteLine("Saving");
                        await _context.SaveChangesAsync();
                           }

                    if(temp != null && temp.QBCustomerName != customer.DisplayName)
                    {
                        temp.QBCustomerName = customer.DisplayName;
                        _context.QBCustomers.Update(temp);
                        Console.WriteLine("Changing customer: " + temp.QBCustomerName + " to " + customer.DisplayName);
                        await _context.SaveChangesAsync();

                    }
                }
            }

            return "success";
        }
    }
}