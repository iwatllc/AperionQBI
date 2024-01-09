using System.Collections.ObjectModel;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Interfaces;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class UpdatePayment : QuickBooksOperation
    {
        private Object l = new object();

        public UpdatePayment(IApplicationDbContext context, IInfoHandler _handler):base(context,_handler){ }

        public bool updatePayment(QBPayment payment)
        {
            
            QueryService<SalesReceipt> qService = new QueryService<SalesReceipt>(serviceContext);


            ReadOnlyCollection<SalesReceipt> receipt = qService.ExecuteIdsQuery($"select * from SalesReceipt where id=\'{payment.id}\'");
            SalesReceipt receiptToUpdate = receipt[0];
            receiptToUpdate.TotalAmt = payment.totalAmount;
            receiptToUpdate.PrivateNote = payment.privateNote;
            receiptToUpdate.TotalAmtSpecified = true;
            receiptToUpdate.Balance = payment.balance;
            receiptToUpdate.BalanceSpecified = true;

            receiptToUpdate.Line.ElementAt(0).Amount = payment.totalAmount;
            receiptToUpdate.Line.ElementAt(0).AmountSpecified = true;
            SalesReceipt result;

            lock (l)
            {
                DataService dService = new DataService(serviceContext);
                result = dService.Update(receiptToUpdate);
            }

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
