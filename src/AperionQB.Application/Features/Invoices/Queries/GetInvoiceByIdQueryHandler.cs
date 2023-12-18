using AperionQB.Application.Abstractions.Messaging;
using AperionQB.Application.Interfaces;
using AperionQB.Application.Mappings;
using AperionQB.Application.Mappings.Invoices;
using AperionQB.Domain.Entities;
using MediatR;

namespace AperionQB.Application.Features.Invoices.Queries;

public class GetInvoiceByIdQueryHandler : IQueryHandler<GetInvoiceByIdQuery, Result<GetInvoiceResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Invoice, GetInvoiceResponse> _mapper = new GetInvoiceMapper();

    public GetInvoiceByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Result<GetInvoiceResponse>> IRequestHandler<GetInvoiceByIdQuery, Result<GetInvoiceResponse>>.Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        Invoice? invoice = await _context.Invoices.FindAsync(request.InvoiceId);

        if (invoice is null)
        {
            return Result<GetInvoiceResponse>.Fail("");
        }

        GetInvoiceResponse response = _mapper.Map(invoice);

        return Result<GetInvoiceResponse>.Success(response);
    }
}
