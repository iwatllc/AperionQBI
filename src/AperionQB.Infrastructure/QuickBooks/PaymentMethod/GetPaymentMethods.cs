using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
	public class GetPaymentMethods : QuickBooksOperation
	{
		private readonly Logger logger;
		public GetPaymentMethods(IApplicationDbContext _context, IInfoHandler _handler):base(_context, _handler)
		{
			logger = new Logger();
		}

		public string getPaymentMethods()
		{
			try
			{
				QueryService<Intuit.Ipp.Data.PaymentMethod> service = new QueryService<Intuit.Ipp.Data.PaymentMethod>(serviceContext);
				ReadOnlyCollection<Intuit.Ipp.Data.PaymentMethod> methods = service.ExecuteIdsQuery($"select * from paymentMethod");

				string[][] paymentMethods = new string[methods.Count()][];
				for (int i = 0; i < methods.Count(); i++)
				{
					string[] temp = new string[2];
					temp[0] = methods.ElementAt(i).Name;
					temp[1] = methods.ElementAt(i).Id;
					paymentMethods[i] = temp;
				}

				return JsonConvert.SerializeObject(paymentMethods);
			}catch(Exception e)
			{
				logger.log(DateTime.Now + ": An error occured while getting payment methods: " + e.Message);
				return "false";
			}
        }
    }
}

