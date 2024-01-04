using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddAllCustomerToQuickBooks
    {
        private readonly IApplicationDbContext _context;
        private readonly Logger _logger;
        public AddAllCustomerToQuickBooks(IApplicationDbContext _context)
        {
            this._context = _context;
            _logger = new Logger();
        }


        public async Task<bool> addCustomerstoQuickBooks()
        {
            try
            {
                _logger.log(DateTime.Now + ": Attempting to add customers to Quick Books");
                IEnumerable<QBCustomerMapping> mappings = _context.BZBQuickBooksCustomerMappings.ToList().Where((map) => (map.qbId == -1));
                if (mappings.Count() != 0)
                {
                    foreach (QBCustomerMapping mapping in mappings)
                    {
                        _logger.log(DateTime.Now + ": Attempting to add company with CompanyID: " + mapping.CompanyID + " to QuickBooks");
                        try
                        {
                            await new AddCustomer(_context).addCustomer();
                        }
                        catch (Exception e)
                        {
                            _logger.log(DateTime.Now + ": Unable to add company with CompanyID: " + mapping.CompanyID + " to Quickbooks: " + e.Message);
                        }

                        _logger.log(DateTime.Now + ": Successfully added company with CompanyID: " + mapping.CompanyID + " to QuickBooks");

                    }
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.log(DateTime.Now + ": There was an issue adding customers to QuickBooks: " + e.Message);
                return false;
            }
        }
    }
}