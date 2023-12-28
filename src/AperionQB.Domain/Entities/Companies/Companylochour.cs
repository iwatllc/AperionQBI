namespace AperionQB.Domain.Entities;

public partial class Companylochour
{
    public int ClasedSaturday { get; set; }
    public int ClosedFriday { get; set; }
    public int ClosedMonday { get; set; }
    public int Closedsunday { get; set; }
    public int ClosedThursday { get; set; }
    public int ClosedTuesday { get; set; }
    public int ClosedWednesday { get; set; }
    public int CompanyLocationId { get; set; }
    public DateTime Created { get; set; }
    public string FridayEnd { get; set; } = null!;
    public string FridayStart { get; set; } = null!;
    public int Id { get; set; }
    public DateTime Modified { get; set; }
    public string MondayEnd { get; set; } = null!;
    public string MondayStart { get; set; } = null!;
    public string SaturdayEnd { get; set; } = null!;
    public string SaturdayStart { get; set; } = null!;
    public string SundayEnd { get; set; } = null!;
    public string SundayStart { get; set; } = null!;
    public string ThursdayEnd { get; set; } = null!;
    public string ThursdayStart { get; set; } = null!;
    public string TuesdayEnd { get; set; } = null!;
    public string TuesdayStart { get; set; } = null!;
    public string WednesdayEnd { get; set; } = null!;
    public string WednesdayStart { get; set; } = null!;
}
