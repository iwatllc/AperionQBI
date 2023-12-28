namespace AperionQB.Domain.Entities;

public partial class InvoiceItemType
{
    public DateTime Created { get; set; }
    public int Id { get; set; }

    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;

    public bool? Status { get; set; }
}
