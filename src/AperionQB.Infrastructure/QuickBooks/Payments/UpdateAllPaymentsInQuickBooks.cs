using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using Intuit.Ipp.Data;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class UpdateAllPaymentsInQuickBooks
	{
		IApplicationDbContext _context;
		public UpdateAllPaymentsInQuickBooks(IApplicationDbContext context)
		{
			_context = context; 
		}

		public async Task<bool> UpdateAllPaymentsInQB()
		{
			//gets all of the transactions that need updated
			IEnumerable<QBUpdateTransactions> transactions = null;

			try
			{
				transactions = _context.QBUpdateTransactions.ToList().Where((trans) => (trans.updateBool == false && trans.datePosted == null));
			}catch (Exception e)
			{
				Console.WriteLine(DateTime.UtcNow + ": Could not fetch UpdateTransactions from the database. Check connection and try again: " + e.Message);
				return false; 
			}

			foreach(QBUpdateTransactions transaction in transactions)
            {
				IEnumerable<QBPayments> paymentsToUpdate = null;

				try
				{
					
					paymentsToUpdate = _context.PaymentsToMigrateToIntuit.Where((pmnt) => (pmnt.id == transaction.QBPaymentsID && pmnt.intuitPaymentID != null));
					if(paymentsToUpdate.Count() == 0)
					{
						Console.WriteLine(DateTime.Now + ": Payment with QBPayment ID: " + transaction.QBPaymentsID + " hasn't been pushed to intuit yet. Please check to make sure a valid Customer Mapping is available. Skipping....");
					}

					for (int i = 0; i < paymentsToUpdate.Count(); i++)
					{
						Console.WriteLine(DateTime.Now + ": Updating intuit payment with intuitID: " + paymentsToUpdate.ElementAt(i).intuitPaymentID);

						if (!paymentsToUpdate.ElementAt(i).intuitPaymentID.HasValue)
						{

							throw new Exception("You shouldn't see this as it is not possible to attempt to update a payment without an intuit id");
						}
						QBPayment payment = new GetPayment().getPaymentByID(paymentsToUpdate.ElementAt(i).intuitPaymentID.Value);

						payment.totalAmount = paymentsToUpdate.ElementAt(0).totalAmount;
						payment.privateNote = paymentsToUpdate.ElementAt(0).Memo;

						bool result = new UpdatePayment().updatePayment(payment);

						if (result)
						{
							transaction.datePosted = DateTime.Now;
							transaction.updateBool = true; 
							_context.QBUpdateTransactions.Update(transaction);
							await _context.SaveChangesAsync();
							Console.WriteLine(DateTime.Now + ": Successfully updated payment with intuitID: "  + paymentsToUpdate.ElementAt(0).intuitPaymentID);
						}
					}
				}catch(Exception e)
				{ 
					Console.WriteLine(DateTime.Now + ": Could not find any payments with payment ID: " + transaction.QBPaymentsID  + ". " + e.Message);
				}
			}

			//gets all of the payments that are going to be updated
			//var payments = _context.QBUpdateTransactions.ToList().Where((pmnt) => (pmnt.id))
			return true; 
		}
	}
}

