namespace AperionQB.Domain.Entities;

public partial class Term
{
    public virtual ICollection<CompanyTerms> CompanyTerms { get; set; } = new List<CompanyTerms>();
    public virtual ICollection<ContactTerms> ContactTerms { get; set; } = new List<ContactTerms>();
    public DateTime Created { get; set; }
    public bool Defaultt { get; set; }
    public int Id { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;

    public bool? Status { get; set; }
}
