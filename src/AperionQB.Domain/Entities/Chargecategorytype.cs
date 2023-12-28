namespace AperionQB.Domain.Entities;

public partial class ChargeCategoryType
{
    public int Id { get; set; }

    public int StatusId { get; set; }
    public string Type { get; set; } = null!;
}
