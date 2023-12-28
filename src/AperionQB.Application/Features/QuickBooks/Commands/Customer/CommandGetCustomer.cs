using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Customer
{
	public class CommandGetCustomer : ICommand<QBCustomer>
	{
		public int id;
		public CommandGetCustomer(int id)
		{
			this.id = id;
		}
	}
}

