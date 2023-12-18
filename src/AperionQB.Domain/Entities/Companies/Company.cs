

namespace AperionQB.Domain.Entities;

public partial class Company
{
    public string? Aliases { get; set; }
    public virtual ICollection<ChargeCategory> ChargeCategories { get; set; } = new List<ChargeCategory>();
    
    
    public virtual ICollection<CompanyCommunication> CompanyCommunications { get; set; } = new List<CompanyCommunication>();
    public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
    public virtual ICollection<Companydocument> CompanyDocuments { get; set; } = new List<Companydocument>();
    public virtual ICollection<CompanyLocation> CompanyLocations { get; set; } = new List<CompanyLocation>();
    public virtual ICollection<Companynote> CompanyNotes { get; set; } = new List<Companynote>();
    public virtual ICollection<CompanyTaxCode> CompanyTaxCodes { get; set; } = new List<CompanyTaxCode>();
    public virtual ICollection<CompanyTerms> CompanyTerms { get; set; } = new List<CompanyTerms>();
    public DateTime Created { get; set; }
    public decimal? Discount { get; set; }
    public int Id { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;
    public virtual Contact? PrimaryContact { get; set; }
    public int? PrimaryContactId { get; set; }
    public int StatusId { get; set; }

    
    public string Website { get; set; } = null!;
}
