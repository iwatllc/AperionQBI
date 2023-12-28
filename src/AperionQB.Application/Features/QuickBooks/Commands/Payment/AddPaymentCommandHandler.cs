using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class AddPaymentCommandHandler : ICommandHandler<AddPaymentCommand, int>
	{
        IQuickBooksManager _manager;

		public AddPaymentCommandHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}
        //(int totalAmt, string customerRef, int customerId, string memo
        public async Task<int> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            return _manager.addPayment(request.total,request.lineItemDescription, request.custId,request.memo);

        }
    }
}

