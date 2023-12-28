using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class GetAllPaymentsCommand : ICommand<bool>
	{
		public GetAllPaymentsCommand()
		{
		}
	}
}

