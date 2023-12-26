using System;
using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.QuickBooks.Commands
{
	public class AddPaymentCommand : ICommand<bool>
	{
		public int total;
        public string refType;
        public int custId;
        public string memo;

		public AddPaymentCommand(int TotalAmt,string ReferenceType, int customerID, string PaymentMemo)
        {
			total = TotalAmt;
			refType = ReferenceType;
			custId = customerID;
			memo = PaymentMemo;
        }
	}
}

