namespace AperionQB.Domain.Entities;

public partial class ChargeCategory
{
    public int? ChargeCategoryTypeId { get; set; }
    public virtual Company? Company { get; set; }
    public int? CompanyId { get; set; }
    public virtual ICollection<Companynote> CompanyNotes { get; set; } = new List<Companynote>();
    public virtual Contact? Contact { get; set; }
    public int? ContactId { get; set; }
    public virtual ICollection<ContactNote> ContactNotes { get; set; } = new List<ContactNote>();
    public DateTime Created { get; set; }
    public int DefaultValue { get; set; }
    public int Id { get; set; }

    public virtual ICollection<InvoiceCustomItem> InvoiceCustomItems { get; set; } = new List<InvoiceCustomItem>();
    public DateTime? Modified { get; set; }
    public decimal? Rate { get; set; }
    public int StatusId { get; set; }
    public string Summary { get; set; } = null!;
}
