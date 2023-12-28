using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class UpdatePaymentCommand : ICommand<bool>
	{
		public QBPayment updatedPayment { get; }

		public UpdatePaymentCommand(QBPayment payment)
		{
			updatedPayment = payment;
		}
	}
}

