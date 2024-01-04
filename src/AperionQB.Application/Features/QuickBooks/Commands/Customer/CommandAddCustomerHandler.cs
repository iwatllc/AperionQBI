using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Customer
{
    public class CommandAddCustomerHandler : ICommandHandler<CommandAddCustomer, bool>
    {
        IQuartsJobManager _manager;
        public CommandAddCustomerHandler(IQuartsJobManager mngr)
        {
            _manager = mngr;
        }

        public async Task<bool> Handle(CommandAddCustomer request, CancellationToken cancellationToken)
        {
            return _manager.fireNewCustomersJob();
        }
    }
}