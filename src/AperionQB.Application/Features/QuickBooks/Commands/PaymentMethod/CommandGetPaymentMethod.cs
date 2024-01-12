using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.PaymentMethod
{
	public class CommandGetPaymentMethod : ICommand<string>
	{
		public int id { get; }
		public CommandGetPaymentMethod(int id)
		{
			this.id = id;
		}
	}
}

