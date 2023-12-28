
namespace AperionQB.Domain.Entities;

public partial class InvoiceCustomItem
{
    public virtual ChargeCategory? ChargeCategory { get; set; }
    public int? ChargeCategoryId { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public string? CustomCCName { get; set; }
    public string Description => Details;
    public string Details { get; set; } = null!;
    public int Id { get; set; }

    public virtual Invoice? Invoice { get; set; }
    public int? InvoiceLineId { get; set; }

    public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
    public DateTime? Modified { get; set; }
    public int? ModifiedBy { get; set; }
    public decimal Price => Rate;
    public decimal Qty { get; set; }

    public decimal Quantity => Qty;
    public decimal Rate { get; set; }
    public decimal Total => Quantity * Price;
}
