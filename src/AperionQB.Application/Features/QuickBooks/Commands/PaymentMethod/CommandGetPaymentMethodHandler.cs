using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.PaymentMethod
{
	public class CommandGetPaymentMethodHandler : ICommandHandler<CommandGetPaymentMethod, string>
	{
        private readonly IQuickBooksManager _manager;

        public CommandGetPaymentMethodHandler(IQuickBooksManager _manager)
		{
            this._manager = _manager;
		}

        public async Task<string> Handle(CommandGetPaymentMethod request, CancellationToken cancellationToken)
        {
            return _manager.getPaymentMethod(request.id); ;
        }
    }
}

