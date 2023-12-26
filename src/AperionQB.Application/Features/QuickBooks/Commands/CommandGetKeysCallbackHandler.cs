using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
    public class CommandGetKeysCallbackHandler : ICommandHandler<CommandGetKeysCallback, bool>
    {
        public IQuickBooksManager _manager;
        public CommandGetKeysCallbackHandler(IQuickBooksManager mngr)
        {
            _manager = mngr;
        }

        public async Task<bool> Handle(CommandGetKeysCallback request, CancellationToken cancellationToken)
        {
            return await _manager.getKeysCallback(request.code, request.realmId);
        }

        
    }
}

