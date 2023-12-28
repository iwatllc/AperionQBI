namespace AperionQB.Domain.Entities;

public partial class CompanyTaxCode
{
    public virtual Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public virtual TaxCode TaxCode { get; set; } = null!;
    public int Taxcodeid { get; set; }
}
