using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class DeleteAllFlaggedPayments
    {
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        private readonly Logger _logger;
        public DeleteAllFlaggedPayments(IApplicationDbContext _context, IInfoHandler _handler)
        {
            this._context = _context;
            this._handler = _handler;
            this._logger = new Logger();
        }

        public async Task<bool> deleteAllFlaggedPaymentsFromQuickBooks()
        {
            //only get payments that were newly marked for deletion (marked for deletion but not deleteion not 12processed in qb already)
            var payments = _context.PaymentsToMigrateToIntuit.ToList().Where(((pmnt) => (pmnt.DeletedBool == true && pmnt.DeletedByQBIDate == null)));
            DeletePayment deletePayment = new DeletePayment(_context, _handler);
            
            foreach (QBPayments payment in payments)
            {
                _logger.log(DateTime.Now + ": About to delete payment with QBPaymentID: " + payment.id);

                //only tell intuit to delete the payment if it has been pushed to intuit already
                if (payment.intuitPaymentID.HasValue)
                {
                    deletePayment.deletePayment(payment.intuitPaymentID.Value);
                }

                payment.DeletedByQBIDate = DateTime.Now;
                _context.PaymentsToMigrateToIntuit.Update(payment);

                await _context.SaveChangesAsync();
                _logger.log(DateTime.Now + ": Successfully deleted payment with QBPaymentID: " + payment.id);
            }

            return false;
        }
    }
}