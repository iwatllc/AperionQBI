using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands;

public class CommandTestQbConnectionHandler : ICommandHandler<CommandTestQbConnection, bool>
{
    public readonly IQuickBooksManager _manager;
    public CommandTestQbConnectionHandler(IQuickBooksManager _manager)
    {
        this._manager = _manager;
    }

    public async Task<bool> Handle(CommandTestQbConnection request, CancellationToken cancellationToken)
    {
        return  _manager.testQbConnection();
    }

}