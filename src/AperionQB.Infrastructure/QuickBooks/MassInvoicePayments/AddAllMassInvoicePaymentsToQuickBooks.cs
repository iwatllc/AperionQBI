using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddAllMassInvoicePaymentsToQuickBooks
    {
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        private readonly Logger _logger;
        public AddAllMassInvoicePaymentsToQuickBooks(IApplicationDbContext _context, IInfoHandler _handler)
        {
            this._context = _context;
            this._handler = _handler;
            _logger = new Logger();
        }


        public async Task<bool> addAllMassInvoicePaymentstoQuickBooks()
        {
            var masspayments = _context.QBMassInvoicePayments.ToList().Where(((pmnt) => (pmnt.intuitPaymentID == null && pmnt.DeletedBool == false)));
            AddPayment addPayment = new AddPayment(_context, _handler);
            List<int> paymentsIds = new List<int>();

            foreach (QBMassInvoicePayment payment in masspayments)
            {
                _logger.log(DateTime.UtcNow + ": Found ID: " + payment.id + " BZB Customer ID: " + payment.BZBCompanyID + " Memo: " + payment.Memo);
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
                    Console.WriteLine("Payment ID: "+ payment.id);
                    int paymentType = _context.Massinvoicepayments.ToList().Where((pay) => (pay.Id == payment.massInvoicePaymentID)).First().Paymenttypeid;
                    Decimal totalAmount = _context.Massinvoicepayments.ToList()
                        .Where((pay) => (pay.Id == payment.massInvoicePaymentID)).First().Amount;
                    
                    int result = addPayment.addPayment(totalAmount, (int)mapping.qbId, paymentType.ToString(), payment.Memo, payment.massInvoicePaymentID.ToString());

                    if (result == -1)
                    {
                        return false;
                    }

                    payment.intuitPaymentID = result;


                    _context.QBMassInvoicePayments.Update(payment);
                    await _context.SaveChangesAsync();
                    _logger.log(DateTime.UtcNow + ": Payment with Payment ID: " + payment.id + " has been successfully pushed to QuickBooks");

                }
            }

            return true;
        }
    }
}