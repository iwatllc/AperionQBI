using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Customer
{
	public class CommandAddCustomer : ICommand<bool>
	{
		public string firstName, lastName, primaryEmailAddress, BillAddrL1, City, displayName;
		public CommandAddCustomer(string firstName, string lastName, string primaryEmailAddress, string BillAddrL1, string City, string displayName)
		{
			this.firstName = firstName;
			this.lastName = lastName;
			this.primaryEmailAddress = primaryEmailAddress;
			this.BillAddrL1 = BillAddrL1;
			this.City = City;
			this.displayName = displayName;
		}
	}
}

