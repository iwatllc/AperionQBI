using System;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Domain.Entities.QuickBooks;

namespace AperionQB.Application.Interfaces
{
	public interface IQuickBooksManager
	{

		string getKeys();

		public Task<bool> getKeysCallback(string code, string realmId);

		bool getAllCustomers();

		QBCustomer getCustomer(int id);

		int addPayment(int totalAmt, string customerRef, int customerId, string memo);

		bool addInvoice();

		QBPayment getPayment(int id);

		QBInvoice getInvoice();

		bool deletePayment(int id);

		public Task<bool> refreshAccessTokens();

		bool updatePayment(QBPayment payment);

		bool getAllPayments();

		bool addCustomer(string firstName, string lastName, string primaryEmailAddress, string BillAddrL1, string City, string displayName);

    }
}

