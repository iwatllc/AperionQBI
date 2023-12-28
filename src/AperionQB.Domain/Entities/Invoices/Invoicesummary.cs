namespace AperionQB.Domain.Entities;

public partial class Invoicesummary
{
    public decimal Charges { get; set; }
    public int? Companyid { get; set; }
    public string? Companyname { get; set; }
    public int? Contactid { get; set; }
    public string? Contactname { get; set; }
    public DateTime? Created { get; set; }
    public int Createdby { get; set; }
    public DateOnly? Due { get; set; }
    public int Id { get; set; }

    public string? Identifier { get; set; }

    public DateOnly? InvoiceDate { get; set; }
    public decimal? Nontaxable { get; set; }
    public string? Notes { get; set; }
    public decimal? Numignored { get; set; }
    public long Numlines { get; set; }
    public decimal Payments { get; set; }
    public string? Ponumber { get; set; }
    public decimal? Taxable { get; set; }
    public decimal? Taxes { get; set; }
    public sbyte? Void { get; set; }
}
