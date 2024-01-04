using System;
namespace AperionQB.Domain.Entities.BZBQB
{
    public class QBUpdateTransactions
    {
        public int id { get; set; }
        public int QBPaymentsID { get; set; }
        public bool updateBool { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedUser { get; set; }
        public DateTime? datePosted { get; set; }
    }
}