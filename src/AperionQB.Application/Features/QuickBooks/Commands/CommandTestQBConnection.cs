using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands;

public class CommandTestQbConnection : ICommand<bool>
{
    public readonly int id;
    public CommandTestQbConnection(int id)
    {
        this.id = id;
    }
}