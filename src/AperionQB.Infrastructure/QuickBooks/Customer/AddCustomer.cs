using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddCustomer : QuickBooksOperation
    {
        private readonly IApplicationDbContext _context;
        private readonly Logger logger;
        public AddCustomer(IApplicationDbContext _context) { this._context = _context; logger = new Logger(); }

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
                AperionQB.Domain.Entities.CompanyCommunication? comms = _context.Companycomms.Where((comms) => (comms.CompanyId == mapping.CompanyID && comms.CommunicationTypeId == 2)).FirstOrDefault();
                AperionQB.Domain.Entities.CompanyLocation? loc = _context.Companylocs.Where((loc) => loc.CompanyId == mapping.CompanyID).FirstOrDefault();

                firstName = (company != null && company.Name != null) ? company.Name : "N/A";
                lastName = "N/A";

                primaryEmailAddress = (comms != null && comms.Value != null) ? comms.Value : "noaddressavailable@gmail.com";
                BillAddrL1 = (loc != null && loc.Address1 != null) ? loc.Address1 : "N/A";
                City = (loc != null && loc.City != null) ? loc.City : "N/A";
                displayName = firstName + " " + lastName;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + "An error has occured while fetching from the database: " + e.Message);
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
                return false;
            }
            return false;
        }
    }
}