using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Features.QuickBooks.Commands;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class GetAllPayments : QuickBooksOperation
    {
        public bool getAllPayments()
        {
            QueryService<SalesReceipt> service = new QueryService<SalesReceipt>(serviceContext);
            ReadOnlyCollection<SalesReceipt> receipt = service.ExecuteIdsQuery($"select * from SalesReceipt");
            return true;
        }
    }
}

