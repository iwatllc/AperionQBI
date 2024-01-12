using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class CommandGetCustomersHandler : ICommandHandler<CommandGetCustomers, string[][]>
	{
        public IQuickBooksManager _manager;
		public CommandGetCustomersHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public async Task<string[][]> Handle(CommandGetCustomers request, CancellationToken cancellationToken)
        {
            return _manager.getAllCustomers();
        }
    }
}

