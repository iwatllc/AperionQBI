using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class CommandRefreshAccessTokensHandler : ICommandHandler<CommandRefreshAccessTokens, bool>
	{
        IQuickBooksManager _manager;
		public CommandRefreshAccessTokensHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public Task<bool> Handle(CommandRefreshAccessTokens request, CancellationToken cancellationToken)
        {
            return _manager.refreshAccessTokens();
        }
    }
}

