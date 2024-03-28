namespace AperionQB.Domain.Entities;

public partial class CompanyContact
{
    public virtual Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public int? CompanyJobClassId { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public int? DepartmentId { get; set; }
    public int Id { get; set; }
    public virtual CompanyLocation? Location { get; set; }
    public int? LocationId { get; set; }

    public DateTime Modified { get; set; }
    public int? Modifiedby { get; set; }
    public int StatusId { get; set; }
    public virtual Contact? Supervisor { get; set; }
    public int? SupervisorId { get; set; }

    public string? Title { get; set; }
}
