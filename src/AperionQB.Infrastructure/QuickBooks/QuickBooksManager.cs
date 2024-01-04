using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.QuickBooks.Payments;
using AperionQB.Infrastructure.QuickBooks.TokenManagement;

namespace AperionQB.Infrastructure.QuickBooks
{
    public class QuickBooksManager : IQuickBooksManager
    {
        QuickBooksKeyActions actions;
        GetAuthURL url;
        AddCustomer customer;
        AddPayment add;
        GetAllCustomers getcustomers;
        GetCustomer getcustomer;
        GetPayment payment;
        DeletePayment delete;
        GetAllPayments getAllPaymentsFromIntuit;
        UpdatePayment update;

        public QuickBooksManager()
        {
            delete = new DeletePayment();
            getAllPaymentsFromIntuit = new GetAllPayments();
            update = new UpdatePayment();
            actions = new QuickBooksKeyActions();
            url = new GetAuthURL();
            add = new AddPayment();
            getcustomers = new GetAllCustomers();
            getcustomer = new GetCustomer();
            payment = new GetPayment();

        }

        public string getKeys()
        {
            return url.getAuthURL();
        }

        async Task<bool> IQuickBooksManager.getKeysCallback(string code, string realmId)
        {
            await new GetNewTokens().GetAuthTokensAsync(code, realmId);

            return true;
        }

        async Task<bool> IQuickBooksManager.refreshAccessTokens()
        {
            //QuickBooksKeyActions actions = new QuickBooksKeyActions();
            return await actions.refreshAccessTokens();
        }

        public bool addInvoice()
        {
            throw new NotImplementedException();
        }

        public int addPayment(int totalAmt, string lineItemDescription, int customerId, string memo)
        {
            try
            {
                return add.addPayment(totalAmt, customerId, lineItemDescription, memo);
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public bool getAllCustomers()
        {
            try
            {
                getcustomers.getCustomersFromIntuit();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public QBCustomer getCustomer(int id)
        {
            return getcustomer.getCustomerByID(id);
        }

        public QBInvoice getInvoice()
        {
            throw new NotImplementedException();
        }

        public QBPayment getPayment(int id)
        {
            QBPayment p = payment.getPaymentByID(id);
            if (p != null)
            {
                return p;
            }
            return null;
        }

        public bool deletePayment(int paymentID)
        {
            return delete.deletePayment(paymentID);
        }

        public bool updatePayment(QBPayment payment)
        {

            return update.updatePayment(payment);
        }

        public bool getAllPayments()
        {
            return getAllPaymentsFromIntuit.getAllPayments();
        }
    }
}