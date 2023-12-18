namespace AperionQB.Domain.Entities;

public partial class Paymenttype
{
    public DateTime Created { get; set; }
    public bool Defaultpt { get; set; }
    public int Id { get; set; }

    public virtual ICollection<InvoicePayments> Invoicepayments { get; set; } = new List<InvoicePayments>();
    public virtual ICollection<Massinvoicepayment> Massinvoicepayments { get; set; } = new List<Massinvoicepayment>();
    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;

    public bool? Status { get; set; }
}
