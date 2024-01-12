using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using AperionQB.Infrastructure.QuickBooks.Payments;

namespace AperionQB.Infrastructure.QuickBooks;

public class TestQbConnection : QuickBooksOperation
{
    private Logger logger;
    private IApplicationDbContext _context;
    private IInfoHandler _handler;
    public TestQbConnection(IApplicationDbContext _context, IInfoHandler _handler) : base(_context, _handler) {
        logger = new Logger();
        this._context = _context;
        this._handler = _handler;
    }

    public bool testConnection(int testCustomerID)
    {
        try
        {
            Console.WriteLine(DateTime.Now +": Testing connection to QB");
            AddPayment payment = new AddPayment(_context, _handler);
            int resadd = payment.addPayment(0, testCustomerID, "", "Sample payment to check connection status", "0");

            if (resadd == -1)
            {
                return false;
            }

            DeletePayment delpay = new DeletePayment(_context, _handler);
            bool resdel = delpay.deletePayment(resadd);

            return resdel;
        }
        catch (Exception e)
        {
            logger.log(DateTime.Now + ": Failed test connection-> " + e.Message);
            return false;
        }

        return false;
    }
}