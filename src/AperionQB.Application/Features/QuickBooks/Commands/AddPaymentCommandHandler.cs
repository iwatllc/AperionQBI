using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class AddPaymentCommandHandler : ICommandHandler<AddPaymentCommand, bool>
	{
        IQuickBooksManager _manager;

		public AddPaymentCommandHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}
        //(int totalAmt, string customerRef, int customerId, string memo
        public async Task<bool> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            return _manager.addPayment(request.total, request.refType, request.custId, request.memo);

        }
    }
}

