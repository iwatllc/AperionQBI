using AperionQB.Application.Features.Invoices.Queries;
using AperionQB.Domain.Entities;

namespace AperionQB.Application.Mappings.Invoices;

public class GetInvoiceMapper : IMapper<Invoice, GetInvoiceResponse>
{
    public GetInvoiceResponse Map(Invoice obj)
    {
        return new GetInvoiceResponse
        {
            Id = obj.Id,
            InvoiceDate = obj.InvoiceDate?.ToString(),
            Identifier = obj.Identifier,
            CompanyId = obj.CompanyId,
            ContactId = obj.ContactId,
            Ponumber = obj.PoNumber,
            TaxCodeId = obj.TaxCodeId,
            TermsId = obj.TermId,
            Notes = obj.Notes,
            Memo = obj.Memo,
            Created = obj.Created,
            Due = obj.Due.ToString(),
            TotalCharges = obj.TotalCharges,
            TotalPayments = obj.TotalPayments,
            PaidInFull = obj.PaidInFull,
            CompanyName = obj.Company?.Name,
            ContactName = obj.Contact?.Name,
            TaxRate = obj.Taxcode?.Rate,
        };
    }
}
