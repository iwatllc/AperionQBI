using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class AddPaymentCommand : ICommand<int>
	{
		public int total;
        public string lineItemDescription;
        public int custId;
        public string memo;
        public string identifier;
		public AddPaymentCommand(int TotalAmt,string lineDesc, int customerID, string PaymentMemo, string identifier)
        {
			total = TotalAmt;
			lineItemDescription = lineDesc;
			custId = customerID;
			memo = PaymentMemo;
			this.identifier = identifier;
        }
	}
}

