namespace AperionQB.Domain.Entities;

public partial class Massinvoicepayment
{
    public decimal Amount { get; set; }
    public string? Createdby { get; set; }
    public DateTime Createddate { get; set; }
    public DateTime Datepaid { get; set; }
    public int Id { get; set; }

    public string Invoicepaymentids { get; set; } = null!;
    public virtual ICollection<InvoicePayments> Invoicepayments { get; set; } = new List<InvoicePayments>();
    public DateTime? Modified { get; set; }
    public string? Modifiedby { get; set; }
    public string? Notes { get; set; }
    public string? PaymentNumber { get; set; }
    public virtual Paymenttype Paymenttype { get; set; } = null!;
    public int Paymenttypeid { get; set; }
}
