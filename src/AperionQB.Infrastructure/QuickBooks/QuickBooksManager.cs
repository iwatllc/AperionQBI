using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.QuickBooks.PaymentMethod;
using AperionQB.Infrastructure.QuickBooks.Payments;
using AperionQB.Infrastructure.QuickBooks.TokenManagement;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AperionQB.Infrastructure.QuickBooks
{
    public class QuickBooksManager : IQuickBooksManager
    {
        private QuickBooksKeyActions actions;
        private GetAuthURL url;
        private AddCustomer customer;
        private AddPayment add;
        private GetAllCustomers getcustomers;
        private GetCustomer getcustomer;
        private  GetPayment payment;
        private DeletePayment delete;
        private  GetAllPayments getAllPaymentsFromIntuit;
        private UpdatePayment update;
        private TestQbConnection connection;
        private GetPaymentMethods getPaymentMethods;
        private GetPaymentMethod method;
        private SaveCustomersToDatabase save;
        private IApplicationDbContext _context;
        private IInfoHandler _handler;

        public QuickBooksManager(IApplicationDbContext _context, IInfoHandler _handler)
        {
            this._context = _context;
            this._handler = _handler;
            _handler.ensureEntryInDB();

            method = new GetPaymentMethod(this._context, this._handler);
            getPaymentMethods = new GetPaymentMethods(this._context,this._handler);
            save = new SaveCustomersToDatabase(this._context, this._handler);
            connection = new TestQbConnection(this._context,this._handler);
            delete = new DeletePayment(this._context, this._handler);
            getAllPaymentsFromIntuit = new GetAllPayments(this._context, this._handler);
            update = new UpdatePayment(this._context, this._handler);
            actions = new QuickBooksKeyActions(this._context, this._handler);
            url = new GetAuthURL(this._context, this._handler);
            add = new AddPayment(this._context, this._handler);
            getcustomers = new GetAllCustomers(this._context, this._handler);
            getcustomer = new GetCustomer(this._context, this._handler);
            payment = new GetPayment(this._context, this._handler);
        }

        public string getPaymentMethod(int id)
        {
            return method.getPaymentMethod(id);
        }

        public string getKeys()
        {
            return url.getAuthURL();
        }

        public bool testQbConnection()
        {
            return connection.testConnection();
        }


        async Task<bool> IQuickBooksManager.getKeysCallback(string code, string realmId)
        {
            await new GetNewTokens(_handler).GetAuthTokensAsync(code, realmId);

            return true;
        }

        public bool updateClientInfo(string clientID, string clientSecret, string callbackURL)
        {        
            _handler.updateIntuitInfo(clientID, clientSecret, callbackURL);
            return false; 
        }

        async Task<bool> IQuickBooksManager.refreshAccessTokens()
        {
            return await actions.refreshAccessTokens();
        }

        public bool addInvoice()
        {
            throw new NotImplementedException();
        }

        public string getAllPaymentMethods()
        {
            return getPaymentMethods.getPaymentMethods();
        }

        public int addPayment(int totalAmt, string lineItemDescription, int customerId, string memo, string identifier)
        {
            try
            {
                return add.addPayment(totalAmt, customerId, lineItemDescription, memo, identifier);
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public string[][] getAllCustomers()
        {
            try
            {
                String tmp =  save.saveCustomersToDatabase().Result;
                String[][] temp = new String[1][];
                temp[0] = new String[1];
                temp[0][0] = tmp;
                return temp;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public string getCustomer(int id)
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