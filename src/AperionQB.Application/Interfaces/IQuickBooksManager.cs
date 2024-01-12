using AperionQB.Application.Features.QuickBooks.Commands;

namespace AperionQB.Application.Interfaces
{
    public interface IQuickBooksManager
    {

        string getKeys();

        public Task<bool> getKeysCallback(string code, string realmId);

        string[][] getAllCustomers();

        bool updateClientInfo(string clientID, string clientSecret, string callbackURL);

        string getCustomer(int id);

        int addPayment(int totalAmt, string customerRef, int customerId, string memo, string identifier);

        bool addInvoice();

        string getPaymentMethod(int id);

        public bool testQbConnection(int testCustomerID);

        QBPayment getPayment(int id);

        QBInvoice getInvoice();

        bool deletePayment(int id);

        public Task<bool> refreshAccessTokens();

        bool updatePayment(QBPayment payment);

        bool getAllPayments();

        string getAllPaymentMethods();
    }
}