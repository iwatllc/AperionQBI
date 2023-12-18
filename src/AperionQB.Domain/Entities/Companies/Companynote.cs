namespace AperionQB.Domain.Entities;

public partial class Companynote
{
    public int Billable { get; set; }
    public int Billed { get; set; }
    public virtual ChargeCategory? Chargecategory { get; set; }
    public int? Chargecategoryid { get; set; }
    public virtual Company Company { get; set; } = null!;
    public int Companyid { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Enddate { get; set; }
    public int Id { get; set; }
    public virtual InvoiceLineItem? Invoiceline { get; set; }
    public int? Invoicelineid { get; set; }
    public virtual ICollection<InvoiceLineItem> Invoicelineitems { get; set; } = new List<InvoiceLineItem>();
    public int Locked { get; set; }
    public DateTime? Modified { get; set; }
    public string Note { get; set; } = null!;
    public DateTime? Startdate { get; set; }
    public bool? Timesheet { get; set; }
    public decimal Timespent { get; set; }
    public int Userid { get; set; }
}
