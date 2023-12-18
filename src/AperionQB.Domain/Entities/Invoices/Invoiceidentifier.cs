namespace AperionQB.Domain.Entities;

public partial class InvoiceIdentifier
{
    public DateTime Created { get; set; }
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public int InvoiceId { get; set; }
    public DateTime? Modified { get; set; }
}
