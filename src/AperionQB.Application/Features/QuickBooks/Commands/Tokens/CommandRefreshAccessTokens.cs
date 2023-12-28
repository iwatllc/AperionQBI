using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class CommandRefreshAccessTokens : ICommand<bool>
	{
		public CommandRefreshAccessTokens()
		{
		}
	}
}

