namespace AperionQB.Domain.Entities;

public partial class CompanyTerms
{
    public virtual Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public virtual Term Terms { get; set; } = null!;
    public int TermsId { get; set; }
}
