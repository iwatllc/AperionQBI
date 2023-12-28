using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class GetPaymentCommand : ICommand<QBPayment>
	{
		public int id;

		public GetPaymentCommand(int id)
		{
			this.id = id;
		}
	}
}

