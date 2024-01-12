using System.Collections.ObjectModel;
using AperionQB.Application.Interfaces;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class GetAllPayments : QuickBooksOperation
    {
       private readonly IApplicationDbContext _context;

		public GetAllPayments(IApplicationDbContext _context, IInfoHandler _handler) : base(_context, _handler) { }

        public bool getAllPayments()
        {
            QueryService<SalesReceipt> service = new QueryService<SalesReceipt>(serviceContext);
            ReadOnlyCollection<SalesReceipt> receipt = service.ExecuteIdsQuery($"select * from SalesReceipt");
            Console.WriteLine("json: " + JsonConvert.SerializeObject(receipt));
            return true;
        }
    }
}