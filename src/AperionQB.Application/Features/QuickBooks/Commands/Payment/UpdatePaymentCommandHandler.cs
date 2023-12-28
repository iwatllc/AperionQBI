using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
	public class UpdatePaymentCommandHandler : ICommandHandler<UpdatePaymentCommand, bool>
	{
        IQuickBooksManager _manager;
		public UpdatePaymentCommandHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            return _manager.updatePayment(request.updatedPayment);
        }
    }
}

