using System;
using System.Collections.ObjectModel;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class DeletePayment : QuickBooksOperation
	{
		public bool deletePayment(int id)
		{
			QueryService<SalesReceipt> qService = new QueryService<SalesReceipt>(serviceContext);
            ReadOnlyCollection<SalesReceipt> receipt = qService.ExecuteIdsQuery($"select * from SalesReceipt where id=\'{id}\'");
			SalesReceipt deleteMe = receipt[0];


            DataService dService = new DataService(serviceContext);
			SalesReceipt result = dService.Delete(deleteMe);

			if(result != null)
			{
				return true;
			}
			return false; 
        }
	}
}

