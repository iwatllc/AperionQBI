using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.QuickBooks;
using AperionQB.Infrastructure.QuickBooks.Payments;
using AperionQB.Infrastructure.QuickBooks.TokenManagement;
using Intuit.Ipp.OAuth2PlatformClient;

namespace AperionQB.Infrastructure.QuickBooks
{
	public class QuickBooksManager : IQuickBooksManager
	{
        QuickBooksKeyActions actions;
		public QuickBooksManager()
		{
            actions = new QuickBooksKeyActions();
		}

        public string getKeys()
        {
            GetAuthURL url = new GetAuthURL();
            return url.getAuthURL();
        }

        async Task<bool> IQuickBooksManager.getKeysCallback(string code, string realmId)
        {
            await actions.GetAuthTokensAsync(code, realmId);

            return true;
        }

        public bool addCustomer(string firstName, string lastName, string primaryEmailAddress, string BillAddrL1, string City, string displayName)
        {
            AddCustomer customer = new AddCustomer();
            return customer.addCustomer( firstName,  lastName,  primaryEmailAddress,  BillAddrL1,  City,  displayName);
        }

        async Task<bool> IQuickBooksManager.refreshAccessTokens()
        {
            QuickBooksKeyActions actions = new QuickBooksKeyActions();
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
                AddPayment add = new AddPayment();
                return add.addPayment(totalAmt, customerId, lineItemDescription, memo);
            }catch (Exception e)
            {
                return -1;
            }

        }

        public bool getAllCustomers()
        {
            try
            {
                GetAllCustomers getcustomers = new GetAllCustomers();
                getcustomers.getCustomersFromIntuit();
            }catch (Exception e)
            {
                return false; 
            }

            return true;
        }

        public QBCustomer getCustomer(int id)
        {
            GetCustomer customer = new GetCustomer();
            return customer.getCustomerByID(id);
        }

        public QBInvoice getInvoice()
        {
            throw new NotImplementedException();
        }

        public QBPayment getPayment(int id)
        {
            GetPayment payment = new GetPayment();
            QBPayment p = payment.getPaymentByID(id);
            if (p != null)
            {
                return p;
            }
            return null;
        }

        public bool deletePayment(int paymentID)
        {
            DeletePayment delete = new DeletePayment();
            return delete.deletePayment(paymentID);
        }

        public bool updatePayment(QBPayment payment)
        {
            UpdatePayment update = new UpdatePayment();
            return update.updatePayment(payment);
        }

        public bool getAllPayments()
        {
            GetAllPayments getAllPayments = new GetAllPayments();
            return getAllPayments.getAllPayments();
        }
    }
}

