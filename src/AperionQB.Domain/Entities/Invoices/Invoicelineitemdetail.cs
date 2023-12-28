namespace AperionQB.Domain.Entities;

public partial class Invoicelineitemdetail
{
    public DateTime? Actiondate { get; set; }
    public int Actionid { get; set; }
    public string? Actionnotes { get; set; }
    public decimal Amount { get; set; }
    public string? Bevent { get; set; }
    public string? Chargecategoryid { get; set; }
    public int? Companynoteid { get; set; }
    public int? Contactnoteid { get; set; }
    public string? Customccname { get; set; }
    public string Customdetails { get; set; } = null!;
    public int? Custominvoiceid { get; set; }
    public string? Custominvoicetypeid { get; set; }
    public string Customqty { get; set; } = null!;
    public string Customrate { get; set; } = null!;
    public string Eventabbr { get; set; } = null!;
    public int Id { get; set; }

    public string? Identifier { get; set; }
    public sbyte Ignored { get; set; }
    public int Invoiceid { get; set; }
    public int? Projecttaskactionid { get; set; }
    public decimal? Rate { get; set; }
    public string? Summary { get; set; }
    public sbyte? Taxable { get; set; }
    public string? Technician { get; set; }
    public int? Ticketactionid { get; set; }

    public int? Ticketchargeid { get; set; }
    public int? Ticketpartid { get; set; }
    public decimal? Timespent { get; set; }
    public int? Todoactionid { get; set; }
}
