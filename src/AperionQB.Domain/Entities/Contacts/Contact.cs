
namespace AperionQB.Domain.Entities;

public partial class Contact
{

    public DateOnly? Birthdate { get; set; }
    public virtual ICollection<ChargeCategory> ChargeCategories { get; set; } = new List<ChargeCategory>();


    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
    public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
    public virtual ICollection<ContactAddress> ContactAddress { get; set; } = new List<ContactAddress>();
    public virtual ICollection<ContactCommunication> ContactCommunications { get; set; } = new List<ContactCommunication>();
    public virtual ICollection<ContactImageDocument> ContactImageDocuments { get; set; } = new List<ContactImageDocument>();
    public virtual ICollection<ContactNote> ContactNotes { get; set; } = new List<ContactNote>();
    public virtual ICollection<ContactTaxCode> ContactTaxCodes { get; set; } = new List<ContactTaxCode>();
    public virtual ICollection<ContactTerms> ContactTerms { get; set; } = new List<ContactTerms>();
    public DateTime Created { get; set; }
    public string? FirstName { get; set; }
    public string? Gender { get; set; }
    public int Id { get; set; }
    public string? Identifier { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? Modified { get; set; }

    public string Name
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public string? Nickname { get; set; }
    public string? Notes { get; set; }
    public int StatusId { get; set; }
    public string? Suffix { get; set; }
    public virtual ICollection<CompanyContact> Supervisors { get; set; } = new List<CompanyContact>();

}
