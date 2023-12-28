namespace AperionQB.Domain.Entities;

public partial class Status
{
    public DateTime Created { get; set; }
    public int Id { get; set; }

    public DateTime Modified { get; set; }
    public int Statusid { get; set; }
    public string Value { get; set; } = null!;
}
