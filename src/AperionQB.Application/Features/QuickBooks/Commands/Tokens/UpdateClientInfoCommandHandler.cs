using System;
using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;

namespace AperionQB.Application.Features.QuickBooks.Commands.Tokens
{
	public class UpdateClientInfoCommandHandler : ICommandHandler<UpdateClientInfoCommand, bool>
	{
		private IQuickBooksManager _manager;
		public UpdateClientInfoCommandHandler(IQuickBooksManager _manager)
		{
			this._manager = _manager;
		}

		public async Task<bool> Handle(UpdateClientInfoCommand request, CancellationToken cancellationToken)
		{
			return _manager.updateClientInfo(request.getSecret(), request.getclientID(), request.getCallbackURL());
		}
	}
}

