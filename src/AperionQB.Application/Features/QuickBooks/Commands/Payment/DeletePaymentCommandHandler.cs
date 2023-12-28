using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class DeletePaymentCommandHandler : ICommandHandler<DeletePaymentCommand, bool>
	{
        public IQuickBooksManager _manager;
		public DeletePaymentCommandHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            return _manager.deletePayment(request.id);
        }
    }
}

