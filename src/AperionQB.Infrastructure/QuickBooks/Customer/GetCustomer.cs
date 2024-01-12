using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Features.QuickBooks.Commands;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using AperionQB.Application.Interfaces;


namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class GetCustomer : QuickBooksOperation
    { 
        private readonly IApplicationDbContext _context;
        public GetCustomer(IApplicationDbContext context, IInfoHandler handler): base(context, handler)
        {
        }


        public string getCustomerByID(int id)
        {
            QueryService<Customer> service = new QueryService<Customer>(serviceContext);


            ReadOnlyCollection<Customer> customers = service.ExecuteIdsQuery($"select * from Customer where id=\'{id}\'");
            if(customers.Count() == 0)
            {
                return "Not Mapped";
                 
            }
            return customers.ElementAt(0).GivenName;
        }
    }
}

