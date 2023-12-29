using System;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using Microsoft.EntityFrameworkCore;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class AddAllPaymentsToQuickBooks
    {
        private readonly IApplicationDbContext _context;
        public AddAllPaymentsToQuickBooks(IApplicationDbContext _context)
        {
            this._context = _context;
        }


        public async Task<bool> addAllPaymentstoQuickBooks()
		{
            var payments = _context.PaymentsToMigrateToIntuit.ToList().Where(((pmnt) => (pmnt.intuitPaymentID == null)));
            AddPayment addPayment = new AddPayment();
            List<int> paymentsIds = new List<int>();

            foreach (QBPayments payment in payments)
            {
                Console.WriteLine(DateTime.UtcNow + ": Found ID: " + payment.id + " BZB Customer ID: " + payment.BZBCustomerID + " Payment Amount: " + payment.totalAmount + " Memo: " + payment.Memo);
                QBCustomerMapping mapping = null;
                try
                {
                     mapping = _context.BZBQuickBooksCustomerMappings.Where((map) => (map.bzbId == payment.BZBCustomerID)).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(DateTime.UtcNow + ": Could not find customer mapping for BZB Customer ID: " + payment.BZBCustomerID + ". Skipping...");
                }


                if (mapping != null)
                {
                    int result = addPayment.addPayment(payment.totalAmount, mapping.qbId, payment.Memo, payment.Memo);

                    if (result == -1)
                    {
                        return false;
                    }

                    payment.intuitPaymentID = result;


                    _context.PaymentsToMigrateToIntuit.Update(payment);
                    await _context.SaveChangesAsync();
                }
            }

            return true;
        }
	}
}

