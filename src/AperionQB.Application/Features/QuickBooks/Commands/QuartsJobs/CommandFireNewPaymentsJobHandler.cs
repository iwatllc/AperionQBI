using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.QuartzJobs
{
    public class CommandFireNewPaymentsJobHander : ICommandHandler<CommandFireNewPaymentsJob>
    {
        IQuartsJobManager manager;
        public CommandFireNewPaymentsJobHander(IQuartsJobManager _manager)
        {
            manager = _manager;
        }

        public Task Handle(CommandFireNewPaymentsJob request, CancellationToken cancellationToken)
        {
            bool result = manager.fireNewPaymentsJob();
            return null;
        }
    }
}