using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;
using AperionQB.Application.Mappings;
using AperionQB.Application.Mappings.Invoices;
using AperionQB.Domain.Entities;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoicesPaginatedQueryHandler : IQueryHandler<GetInvoicesPaginatedQuery, Result<List<GetInvoiceResponse>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Invoice, GetInvoiceResponse> _mapper = new GetInvoiceMapper();

    public GetInvoicesPaginatedQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result<List<GetInvoiceResponse>>> Handle(GetInvoicesPaginatedQuery request, CancellationToken cancellationToken)
    {
        List<Invoice> invoices = _context.Invoices
            .OrderByDescending(inv => inv.InvoiceDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        List<GetInvoiceResponse> response = invoices.ConvertAll((inv) => _mapper.Map(inv));

        return Result<List<GetInvoiceResponse>>.SuccessAsync(response);
    }
}
