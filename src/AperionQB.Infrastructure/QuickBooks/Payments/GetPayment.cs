using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Features.QuickBooks.Commands;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
/**
 * When testing use id 166
 */

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class GetPayment : QuickBooksOperation
	{
		public QBPayment getPaymentByID(int id)
		{
			QueryService<SalesReceipt> service = new QueryService<SalesReceipt>(serviceContext);

		
			ReadOnlyCollection<SalesReceipt> receipt = service.ExecuteIdsQuery($"select * from SalesReceipt where id=\'{id}\'");
		
			QBPayment payment = new QBPayment(Int32.Parse(receipt[0].Id), receipt[0].TotalAmt, receipt[0].PrivateNote, receipt[0].Balance);
			return payment;
		}
    }
}

