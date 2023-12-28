using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetInvoiceResponse>> GetInvoice(int id)
    {
        var invoiceResult = await _mediator.Send(new GetInvoiceByIdQuery(id));

        if (invoiceResult.Succeeded)
        {
            return Ok(invoiceResult.Data);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<List<GetInvoiceResponse>>> GetInvoicesPaginated(int pageNumber)
    {
        if (pageNumber == null || pageNumber == 0) {
            pageNumber = 1;
        }
        var query = new GetInvoicesPaginatedQuery(pageNumber, 50);
        var invoicesListResult = await _mediator.Send(query);
        if (invoicesListResult.Succeeded)
        {
            return Ok(invoicesListResult.Data);
        }
        else
        {
            return BadRequest(invoicesListResult.Messages);
        }

    }

}
