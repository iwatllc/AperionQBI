using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.MassInvoicePayment;

public class CommandFireCreateMassInvoicePaymentJobHandler : ICommandHandler<CommandFireCreateMassInvoicePaymentJob, bool>
{
    private IQuartsJobManager _manager;
    public CommandFireCreateMassInvoicePaymentJobHandler(IQuartsJobManager _manager)
    {
        this._manager = _manager;
    }
    
    public async Task<bool> Handle(CommandFireCreateMassInvoicePaymentJob request, CancellationToken cancellationToken)
    {
        return _manager.fireNewMassInvoicePaymentsJob();
    }
}