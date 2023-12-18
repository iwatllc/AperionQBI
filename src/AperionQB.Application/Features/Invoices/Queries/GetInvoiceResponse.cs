namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoiceResponse
{
    public int? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int? ContactId { get; set; }
    public string? ContactName { get; set; }
    public DateTime? Created { get; set; }
    public int CreatedBy { get; set; }
    public string? Due { get; set; }
    public int Id { get; set; }

    public string? Identifier { get; set; }

    public string? InvoiceDate { get; set; }
    public string? Memo { get; set; }
    public string? Notes { get; set; }
    public bool PaidInFull { get; set; }
    public string? Ponumber { get; set; }

    public int? TaxCodeId { get; set; }

    public decimal? TaxRate { get; set; }
    public int? TermsId { get; set; }
    public decimal TotalCharges { get; set; }
    public decimal TotalPayments { get; set; }
    public sbyte? Void { get; set; }

    public string GetClientName()
    {
        if (CompanyName != null && ContactName != null)
        {
            return $"{ContactName}, {CompanyName}";
        }
        else if (CompanyName != null)
        {
            return CompanyName;
        }
        else if (ContactName != null)
        {
            return ContactName;
        }
        else
        {
            return string.Empty;
        }
    }
}
