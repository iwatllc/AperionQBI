using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using Intuit.Ipp.QueryFilter;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks.PaymentMethod
{
	public class GetPaymentMethod : QuickBooksOperation
	{
		Logger logger;
		public GetPaymentMethod(IApplicationDbContext _context, IInfoHandler _handler) : base(_context, _handler){ logger = new Logger(); }

        public string getPaymentMethod(int id)
        {
            try
            {
                QueryService<Intuit.Ipp.Data.PaymentMethod> service = new QueryService<Intuit.Ipp.Data.PaymentMethod>(serviceContext);
                Intuit.Ipp.Data.PaymentMethod methods = service.ExecuteIdsQuery($"select * from paymentMethod where id=\'{id}\'").ElementAt(0);

                return methods.Name;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": An error occured while getting payment method: " + e.Message);
                return "false";
            }
        }
    }
}

