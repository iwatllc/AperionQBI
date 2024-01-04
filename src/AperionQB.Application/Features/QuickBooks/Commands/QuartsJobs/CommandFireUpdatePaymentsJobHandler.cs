using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs
{
    public class CommandFireUpdatePaymentsJobHandler : ICommandHandler<CommandFireUpdatePaymentsJob>
    {
        private readonly IQuartsJobManager _manager;
        public CommandFireUpdatePaymentsJobHandler(IQuartsJobManager manager)
        {
            _manager = manager;
        }

        public Task Handle(CommandFireUpdatePaymentsJob request, CancellationToken cancellationToken)
        {
            _manager.fireUpdatePaymentsJob();
            return null;
        }
    }
}