namespace AperionQB.Domain.Entities;

public partial class CompanyJobClass
{
    public int? ChargeCategoryId { get; set; }
    public int CompanyId { get; set; }
    public DateTime? Created { get; set; }
    public int Id { get; set; }
    public int JobClassId { get; set; }
    public DateTime? Modified { get; set; }
    public decimal Rate { get; set; }

    public int StatusId { get; set; }
}
