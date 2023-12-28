using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class GetPaymentCommandHandler : ICommandHandler<GetPaymentCommand, QBPayment>
	{
        IQuickBooksManager _manager;

        public GetPaymentCommandHandler(IQuickBooksManager mgr)
		{
            _manager = mgr;
		}

        public async Task<QBPayment> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
        {
            return _manager.getPayment(request.id);
        }
    }
}

