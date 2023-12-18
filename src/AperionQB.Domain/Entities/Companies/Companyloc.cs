namespace AperionQB.Domain.Entities;

public partial class CompanyLocation
{
    public string Address1 { get; set; } = null!;
    public string Address2 { get; set; } = null!;
    public string Attention { get; set; } = null!;
    public int Billing { get; set; }
    public string City { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
    public int CompanyId { get; set; }
    public DateTime Created { get; set; }
    public int DepartmentId { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public string Name { get; set; } = null!;
    public int PrimaryLocation { get; set; }
    public string State { get; set; } = null!;

    public int StatusId { get; set; }
    public string Zip { get; set; } = null!;

    public string? Zipplus { get; set; }
}
