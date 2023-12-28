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


        public bool addAllPaymentstoQuickBooks()
		{
            var payments = _context.PaymentsToMigrateToIntuit.ToList().Where(((pmnt) => (pmnt.intuitPaymentID == null)));
            AddPayment addPayment = new AddPayment();
            List<int> paymentsIds = new List<int>();

            foreach (QBPayments payment in payments)
            {
                Console.WriteLine("Found: ID: " + payment.id + " BZB Customer ID: " + payment.BZBCustomerID + " Payment Amount: " + payment.PaymentAmount + " Memo: " + payment.Memo);
                QBCustomerMapping mapping = _context.BZBQuickBooksCustomerMappings.Where((map) => (map.bzbId == payment.BZBCustomerID)).First();
                if (mapping != null)
                {
                    int result = addPayment.addPayment(payment.PaymentAmount, mapping.qbId, payment.Memo, payment.Memo);

                    if (result == -1)
                    {
                        return false;
                    }

                    payment.intuitPaymentID = result;


                    _context.PaymentsToMigrateToIntuit.Update(payment);
                    _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Could not find a customer mapping for BZB Customer with ID: " + payment.BZBCustomerID);
                }

            }

            return true;
        }
	}
}

