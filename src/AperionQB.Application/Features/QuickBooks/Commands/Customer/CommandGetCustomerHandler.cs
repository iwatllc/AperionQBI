using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Customer
{
	public class CommandGetCustomerHandler : ICommandHandler<CommandGetCustomer, QBCustomer>
	{
        IQuickBooksManager _manager;
		public CommandGetCustomerHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public async Task<QBCustomer> Handle(CommandGetCustomer request, CancellationToken cancellationToken)
        {
            return _manager.getCustomer(request.id);
        }
    }
}

