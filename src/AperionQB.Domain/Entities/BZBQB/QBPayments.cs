﻿using System;
namespace AperionQB.Domain.Entities.BZBQB
{
    public partial class QBPayments
    {
        public int id { get; set; }
        public int InvoiceID { get; set; }
        public int InvoicePaymentID { get; set; }
        public int BZBCompanyID { get; set; }
        public decimal totalAmount { get; set; }
        public string? Memo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public bool DeletedBool { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedByDate { get; set; }
        public DateTime? DeletedByQBIDate { get; set; }
        public int? intuitPaymentID { get; set; }

    }
}