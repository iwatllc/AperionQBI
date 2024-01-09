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


        public QBCustomer getCustomerByID(int id)
        {
            QueryService<Customer> service = new QueryService<Customer>(serviceContext);


            ReadOnlyCollection<Customer> customers = service.ExecuteIdsQuery($"select * from Customer where id=\'{id}\'");
            QBCustomer customer = new QBCustomer(Int32.Parse(customers[0].Id), customers[0].GivenName, customers[0].FamilyName, customers[0].PrimaryEmailAddr.Address, customers[0].BillAddr.Line1, customers[0].BillAddr.City, customers[0].DisplayName);
            return customer;
        }
    }
}

