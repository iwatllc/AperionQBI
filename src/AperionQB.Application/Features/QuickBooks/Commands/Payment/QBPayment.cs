using System;
namespace AperionQB.Application.Features.QuickBooks.Commands
{
    [Serializable]
    public class QBPayment
    {
        public decimal id { get; }
        public decimal totalAmount { get; set; }
        public string privateNote { get; set; }
        public decimal balance { get; }

        public QBPayment(int id, decimal totalAmt, string pNote, decimal bal)
        {
            this.id = id;
            totalAmount = totalAmt;
            privateNote = pNote;
            balance = bal;
        }
    }
}
