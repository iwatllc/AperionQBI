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
        TestQbConnection connection;
        GetPaymentMethods getPaymentMethods;
        GetPaymentMethod method;
        private IApplicationDbContext _context;
        private IInfoHandler _handler;

        public QuickBooksManager(IApplicationDbContext _context, IInfoHandler _handler)
        {
            this._context = _context;
            this._handler = _handler;
            _handler.ensureEntryInDB();

            method = new GetPaymentMethod(_context, _handler);
            getPaymentMethods = new GetPaymentMethods(_context, _handler);
            connection = new TestQbConnection(_context, _handler);
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
                return getcustomers.getCustomersFromIntuit();
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