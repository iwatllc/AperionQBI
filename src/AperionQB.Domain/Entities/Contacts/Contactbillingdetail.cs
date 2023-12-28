namespace AperionQB.Domain.Entities;

public partial class Contactbillingdetail
{
    public DateTime? Bdate { get; set; }
    public string Bevent { get; set; } = null!;
    public int? ContactId { get; set; }
    public string? ContactName { get; set; }

    public string EventAbbr { get; set; } = null!;
    public int Id { get; set; }
    public string ItemIdentifier { get; set; } = null!;
    public string? Notes { get; set; }

    public decimal? Rate { get; set; }
    public string? Summary { get; set; }
    public string? Technician { get; set; }
    public decimal? Timespent { get; set; }
}
