using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Payment
{
    
	public class GetAllPaymentsCommandHandler : ICommandHandler<GetAllPaymentsCommand, bool>
	{
        public IQuickBooksManager _manager;

		public GetAllPaymentsCommandHandler(IQuickBooksManager mngr)
		{
            _manager = mngr;
		}

        public Task<bool> Handle(GetAllPaymentsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

