using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Customer
{
	public class CommandAddCustomerHandler : ICommandHandler<CommandAddCustomer, bool>
	{
        IQuickBooksManager _manager;
		public CommandAddCustomerHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public async Task<bool> Handle(CommandAddCustomer request, CancellationToken cancellationToken)
        {
            return _manager.addCustomer(request.firstName, request.lastName, request.primaryEmailAddress, request.BillAddrL1, request.City, request.displayName);
        }
    }
}

