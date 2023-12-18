

namespace AperionQB.Domain.Entities;

public partial class Invoice
{
   

    public virtual Company? Company { get; set; }
    public int? CompanyId { get; set; }
    public virtual Contact? Contact { get; set; }
    public int? ContactId { get; set; }
    public DateTime? Created { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly? Due { get; set; }
    public int Id { get; set; }

    public string? Identifier { get; set; }

    public virtual ICollection<InvoiceCustomItem> InvoiceCustomItems { get; set; } = new List<InvoiceCustomItem>();
    public DateOnly? InvoiceDate { get; set; }
    public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
    public virtual ICollection<InvoicePayments> InvoicePayments { get; set; } = new List<InvoicePayments>();
    public string? Memo { get; set; }
    public string? Notes { get; set; }
    public bool PaidInFull { get; set; }
    public string? PoNumber { get; set; }

    public virtual TaxCode? Taxcode { get; set; }
    public int? TaxCodeId { get; set; }

    public virtual Term? Term { get; set; }
    public int? TermId { get; set; }
    public decimal TotalCharges { get; set; }
    public decimal TotalPayments { get; set; }
    public sbyte? Void { get; set; }
}
