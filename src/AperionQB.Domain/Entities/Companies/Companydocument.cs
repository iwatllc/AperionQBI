namespace AperionQB.Domain.Entities;

public partial class Companydocument
{
    public virtual Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public string? DocumentDescription { get; set; }
    public string DocumentExtension { get; set; } = null!;
    public string DocumentTitle { get; set; } = null!;
    public int Id { get; set; }
    public DateTime Modified { get; set; }

    public int ModifiedBy { get; set; }
}
