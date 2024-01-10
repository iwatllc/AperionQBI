using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddAllPaymentsToQuickBooks
    {
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        private readonly Logger _logger;
        public AddAllPaymentsToQuickBooks(IApplicationDbContext _context, IInfoHandler _handler)
        {
            this._context = _context;
            this._handler = _handler;
            _logger = new Logger();
        }


        public async Task<bool> addAllPaymentstoQuickBooks()
        {
            var payments = _context.PaymentsToMigrateToIntuit.ToList().Where(((pmnt) => (pmnt.intuitPaymentID == null && pmnt.DeletedBool == false)));
            AddPayment addPayment = new AddPayment(_context, _handler);
            List<int> paymentsIds = new List<int>();

            foreach (QBPayments payment in payments)
            {
                _logger.log(DateTime.UtcNow + ": Found ID: " + payment.id + " BZB Customer ID: " + payment.BZBCompanyID + " Payment Amount: " + payment.totalAmount + " Memo: " + payment.Memo);
                QBCustomerMapping? mapping = null;

                try
                {
                    mapping = _context.BZBQuickBooksCustomerMappings.Where((map) => (map.CompanyID == payment.BZBCompanyID)).First();
                }
                catch (Exception)
                {
                    _logger.log(DateTime.UtcNow + ": Could not find customer mapping for BZB Customer ID: " + payment.BZBCompanyID + ". Skipping...");
                }


                if (mapping != null)
                {
                    int result = addPayment.addPayment(payment.totalAmount, (int)mapping.qbId, payment.Memo, payment.Memo);

                    if (result == -1)
                    {
                        return false;
                    }

                    payment.intuitPaymentID = result;


                    _context.PaymentsToMigrateToIntuit.Update(payment);
                    await _context.SaveChangesAsync();
                    _logger.log(DateTime.UtcNow + ": Payment with Payment ID: " + payment.id + " has been successfully pushed to QuickBooks");

                }
            }

            return true;
        }
    }
}