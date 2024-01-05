using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands.Tokens
{
	public class UpdateClientInfoCommand : ICommand<bool>
	{

		readonly string clientID;
		readonly string clientSecret;
		readonly string callbackURL;
		
		public UpdateClientInfoCommand(string clientId, string clientSecret, string callbackURL)
		{
			this.clientID = clientId;
			this.clientSecret = clientSecret;
			this.callbackURL = callbackURL;
		}

		public string getclientID()
		{
			return clientID;
		}

		public string getSecret()
		{
			return clientSecret;
		}

		public string getCallbackURL()
		{
			return callbackURL;
		}
	}
}

