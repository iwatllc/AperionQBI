using AperionQB.Application.Abstractions.Messaging;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoicesPaginatedQuery : IQuery<Result<List<GetInvoiceResponse>>>
{
    public GetInvoicesPaginatedQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
