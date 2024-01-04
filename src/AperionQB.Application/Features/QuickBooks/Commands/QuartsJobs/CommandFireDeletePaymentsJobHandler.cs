using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Features.QuickBooks.Commands.QuartsJobs;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs
{
    public class CommandFireDeletePaymentsJobHandler : ICommandHandler<CommandFireDeletePaymentsJob>
    {
        IQuartsJobManager manager;
        public CommandFireDeletePaymentsJobHandler(IQuartsJobManager _manager)
        {
            manager = _manager;
        }

        public Task Handle(CommandFireDeletePaymentsJob request, CancellationToken cancellationToken)
        {
            manager.fireDeletePaymentsJob();
            return null;
        }
    }
}