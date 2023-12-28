using System.Collections;
//using AperionQB.Application.Features.Invoices.Commands;
using AperionQB.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Infrastructure.QuickBooks;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Customer;
using Newtonsoft.Json;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitGetCustomerByID : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitGetCustomerByID(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerByID([FromQuery]int id)
    {
        QBCustomer result = await _mediator.Send(new CommandGetCustomer(id));
        if (result != null)
        {
            return Ok(JsonConvert.SerializeObject(result));
        }
        else
        {
            return BadRequest("Unable to get customers from intuit. Please check logs and fix the issue");
        }
    }
}
