namespace AperionQB.Domain.Entities;

public partial class ContactTerms
{
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public virtual Term Terms { get; set; } = null!;
    public int TermsId { get; set; }
}
