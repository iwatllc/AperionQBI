using System;
namespace AperionQB.Application.Features.QuickBooks.Commands
{
	[Serializable]
	public class QBCustomer
	{
		public int id { get; }
        public string FirstName { get; }
		public string LastName { get; }
        public string BillAddrL1 { get;}
		public string city { get; }
        public string emailAddress { get; }
		public string displayName { get; }

		public QBCustomer(int id, string firstName, string lastName, string primaryEmailAddress, string BillAddrL1, string City, string displayName)
		{
            this.id = id;
			FirstName = firstName;
			LastName = lastName;
			this.BillAddrL1 = BillAddrL1;
			city = City;
			emailAddress = primaryEmailAddress;

		}
	}
}

