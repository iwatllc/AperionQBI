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

        public bool addCustomer()
        {
            throw new NotImplementedException();
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

        public bool addPayment(int totalAmt, string customerRef, int customerId, string memo)
        {
            try
            {
                AddPayment add = new AddPayment();
                return add.addPayment(totalAmt, customerId, memo);
            }catch (Exception e)
            {
                return false;
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
            throw new NotImplementedException();
        }

        public QBInvoice getInvoice()
        {
            throw new NotImplementedException();
        }

        public QBPayment getPayment()
        {
            throw new NotImplementedException();
        }

        public bool deletePayment(int paymentID)
        {
            throw new NotImplementedException();
        }
    }
}

