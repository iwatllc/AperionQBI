namespace AperionQB.Domain.Entities;

public partial class ContactNote
{
    public int Billable { get; set; }
    public int Billed { get; set; }
    public virtual ChargeCategory? ChargeCategory { get; set; }
    public int? ChargeCategoryId { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Enddate { get; set; }
    public int Id { get; set; }
    public virtual InvoiceLineItem? InvoiceLine { get; set; }
    public int? InvoiceLineId { get; set; }
    public virtual ICollection<InvoiceLineItem> Invoicelineitems { get; set; } = new List<InvoiceLineItem>();
    public int Locked { get; set; }
    public DateTime? Modified { get; set; }
    public string Note { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public bool? Timesheet { get; set; }
    public decimal Timespent { get; set; }
    public int UserId { get; set; }
}
