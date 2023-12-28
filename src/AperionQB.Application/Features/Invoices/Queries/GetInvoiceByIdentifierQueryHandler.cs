using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;
using AperionQB.Application.Mappings;
using AperionQB.Application.Mappings.Invoices;
using AperionQB.Domain.Entities;
using MediatR;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoiceByIdentifierQueryHandler : IQueryHandler<GetInvoiceByIdentifierQuery, Result<GetInvoiceResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Invoice, GetInvoiceResponse> _mapper = new GetInvoiceMapper();

    public GetInvoiceByIdentifierQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Result<GetInvoiceResponse>> IRequestHandler<GetInvoiceByIdentifierQuery, Result<GetInvoiceResponse>>.Handle(GetInvoiceByIdentifierQuery request, CancellationToken cancellationToken)
    {
        Invoice Invoice = _context.Invoices.Where((inv) => (inv.Identifier == request.InvoiceIdentifier)).First();

        GetInvoiceResponse response = _mapper.Map(Invoice);

        return Result<GetInvoiceResponse>.Success(response);
    }
}
