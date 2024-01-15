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

    public bool testConnection()
    {
        try
        {
            GetPaymentMethods methods = new GetPaymentMethods(_context, _handler);
            string res = methods.getPaymentMethods();

            if (res != null && res != "false")
            {
                return true;
            }
        }
        catch (Exception e)
        {
            logger.log(DateTime.Now + ": Failed test connection-> " + e.Message);
            return false;
        }

        return false;
    }
}