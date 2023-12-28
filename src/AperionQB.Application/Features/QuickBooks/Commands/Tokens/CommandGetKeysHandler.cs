using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class CommandGetKeysHandler : ICommandHandler<CommandGetKeys, string>
	{
        public IQuickBooksManager _manager;
        public CommandGetKeysHandler(IQuickBooksManager mngr)
        {
            _manager = mngr;
        }

        public async Task<string> Handle(CommandGetKeys request, CancellationToken cancellationToken)
        {
            return _manager.getKeys();
        }
    }
}

