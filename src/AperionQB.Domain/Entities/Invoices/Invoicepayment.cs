namespace AperionQB.Domain.Entities;

public partial class InvoicePayments
{
    public decimal? Amount { get; set; }
    public DateTime Created { get; set; }
    public string? Createdby { get; set; }
    public DateOnly? DatePaid { get; set; }
    public int Id { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
    public int InvoiceId { get; set; }

    public virtual Massinvoicepayment? MassInvoicePayment { get; set; }
    public int? MassInvoicePaymentId { get; set; }
    public DateTime? Modified { get; set; }
    public string? Modifiedby { get; set; }
    public string? Notes { get; set; }
    public string? PaymentNumber { get; set; }
    public virtual Paymenttype PaymentType { get; set; } = null!;
    public int PaymentTypeId { get; set; }
}
