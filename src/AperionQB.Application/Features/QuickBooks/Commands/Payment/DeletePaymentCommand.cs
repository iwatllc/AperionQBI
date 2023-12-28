using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class DeletePaymentCommand : ICommand<bool>
	{
		public int id { get; }
		public DeletePaymentCommand(int id)
		{
			this.id = id;
		}
	}
}

