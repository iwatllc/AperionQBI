using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Domain.Entities.QuickBooks;

namespace AperionQB.Application.Interfaces
{
	public interface IQuickBooksManager
	{

		string getKeys();

		public Task<bool> getKeysCallback(string code, string realmId);

        bool addCustomer();

		bool getAllCustomers();

		QBCustomer getCustomer(int id);

		bool addPayment(int totalAmt, string customerRef, int customerId, string memo);

		bool addInvoice();

		QBPayment getPayment();

		QBInvoice getInvoice();

		bool deletePayment(int id);

		public Task<bool> refreshAccessTokens();
    }
}

