using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddCustomer : QuickBooksOperation
    {
        private readonly Logger logger;
        public AddCustomer(IApplicationDbContext _context, IInfoHandler _handler) : base(_context, _handler) {logger = new Logger(); }

        /**
         * Example found here: https://github.com/IntuitDeveloper/SampleApp-CRUD-.Net/blob/master/SampleApp_CRUD_.Net/SampleApp_CRUD_.Net/Helper/QBOHelper.cs#L264
         */
        public async Task<bool> addCustomer()
        {
            string firstName;
            string lastName;
            string primaryEmailAddress;
            string BillAddrL1;
            string City;
            string displayName;
            QBCustomerMapping? mapping;

            try
            {

                try
                {
                    mapping = _context.BZBQuickBooksCustomerMappings.Where((map) => (map.qbId == -1)).First();
                }
                catch (Exception e)
                {
                    logger.log(DateTime.Now + ": There are no customers to be added or another error occured: " + e.Message);
                    return false;
                }
                AperionQB.Domain.Entities.Company? company = _context.Companies.Where((company) => company.Id == mapping.CompanyID).FirstOrDefault();
                AperionQB.Domain.Entities.CompanyCommunication? comms = null;
                AperionQB.Domain.Entities.CompanyLocation? loc = null;

                firstName = (company != null && company.Name != null) ? company.Name : "N/A";
                lastName = "N/A";

                string cleanEmail = null;
                if (comms != null)
                {
                    cleanEmail = RemoveWhitespace(comms.Value);
                }

                primaryEmailAddress = (comms != null && cleanEmail != null) ? cleanEmail : "noaddressavailable@gmail.com";
                BillAddrL1 = (loc != null && loc.Address1 != null) ? loc.Address1 : "N/A";
                City = (loc != null && loc.City != null) ? loc.City : "N/A";
                displayName = firstName + " " + lastName;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + "An error has occured while fetching from the databased: " + e.Message);
                return false;
            }


            try
            {
                EmailAddress addr = new EmailAddress();
                addr.Address = primaryEmailAddress;

                PhysicalAddress pAddr = new PhysicalAddress();
                pAddr.City = City;
                pAddr.Line1 = BillAddrL1;

                Customer customer = new Customer();
                customer.GivenName = firstName;
                customer.FamilyName = lastName;
                customer.PrimaryEmailAddr = addr;
                customer.BillAddr = pAddr;
                customer.DisplayName = displayName;

                DataService service = new DataService(serviceContext);
                Customer result = service.Add<Customer>(customer);
                mapping.qbId = Int32.Parse(result.Id);
                _context.BZBQuickBooksCustomerMappings.Update(mapping);

                await _context.SaveChangesAsync();
                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": Unable to add company " + displayName + " to QuickBooks due to: " + e.Message);

                mapping.qbId = -2;
                _context.BZBQuickBooksCustomerMappings.Update(mapping);
                return false;
            }
            return false;
        }
        
        public string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}