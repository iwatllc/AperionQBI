using System.Collections;
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
    public async Task<IActionResult> GetCustomerByID([FromQuery] int id)
    {
        string result = _mediator.Send(new CommandGetCustomer(id)).Result;
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Unable to get customer from intuit. Please check logs and fix the issue");
        }
    }
}