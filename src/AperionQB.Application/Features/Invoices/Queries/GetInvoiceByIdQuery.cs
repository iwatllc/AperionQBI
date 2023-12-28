using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoiceByIdQuery : IQuery<Result<GetInvoiceResponse>>
{
    public GetInvoiceByIdQuery(int invoiceId)
    {
        InvoiceId = invoiceId;
    }

    public int InvoiceId { get; init; }
}
