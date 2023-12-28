using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoiceByIdentifierQuery : IQuery<Result<GetInvoiceResponse>>
{
    public GetInvoiceByIdentifierQuery(string invoiceIdentifier)
    {
        InvoiceIdentifier = invoiceIdentifier;
    }

    public string InvoiceIdentifier { get; init; }
}
