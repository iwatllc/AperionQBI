using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands;

public class CommandGetPaymentMethodsHandler : ICommandHandler<CommandGetPaymentMethods, string>
{
    public readonly IQuickBooksManager _manager;
    public CommandGetPaymentMethodsHandler(IQuickBooksManager _manager)
    {
        this._manager = _manager;
    }

    public async Task<string> Handle(CommandGetPaymentMethods request, CancellationToken cancellationToken)
    {
        return _manager.getAllPaymentMethods(); ;
    }

}