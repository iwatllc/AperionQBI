using System;
using System.Text.Json;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddCustomer : QuickBooksOperation
    {
        /**
         * Example found here: https://github.com/IntuitDeveloper/SampleApp-CRUD-.Net/blob/master/SampleApp_CRUD_.Net/SampleApp_CRUD_.Net/Helper/QBOHelper.cs#L264
         */
        public bool addCustomer(string firstName, string lastName, string primaryEmailAddress, string BillAddrL1, string City, string displayName)
        {
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

                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return false;
            }
            return false;
        }
    }
}

