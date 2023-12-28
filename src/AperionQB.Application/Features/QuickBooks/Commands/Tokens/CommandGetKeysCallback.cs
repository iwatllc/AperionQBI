using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class CommandGetKeysCallback : ICommand<bool>
	{
		public string code;
		public string state;
		public string realmId;
		public CommandGetKeysCallback(string c, string s, string rId)
		{
			code = c;
			state = s;
			realmId = rId;
		}
	}
}

