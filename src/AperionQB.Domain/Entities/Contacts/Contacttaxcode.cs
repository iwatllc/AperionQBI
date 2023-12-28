namespace AperionQB.Domain.Entities;

public partial class ContactTaxCode
{
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public virtual TaxCode TaxCode { get; set; } = null!;
    public int TaxCodeId { get; set; }
}
