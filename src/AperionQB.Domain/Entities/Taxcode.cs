namespace AperionQB.Domain.Entities;

public partial class TaxCode
{
    public virtual ICollection<CompanyTaxCode> CompanyTaxCodes { get; set; } = new List<CompanyTaxCode>();
    public virtual ICollection<ContactTaxCode> ContactTaxCodes { get; set; } = new List<ContactTaxCode>();
    public DateTime Created { get; set; }
    public bool Defaulttc { get; set; }
    public int Id { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public decimal RatePercentage
    {
        get
        {
            return Rate / 100.00M;
        }
    }

    public bool? Status { get; set; }
}
