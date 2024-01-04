using MediatR;
using Microsoft.AspNetCore.Mvc;
using AperionQB.Application.Features.QuickBooks.Commands;

namespace AperionPSD.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitGetAllCustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitGetAllCustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IntuitGetAllCustomers()
    {
        bool result = _mediator.Send(new CommandGetCustomers()).Result;
        if (result)
        {
            return Ok("Got all customers from Intuit");
        }
        else
        {
            return BadRequest("Unable to get customers from intuit. Please check logs and fix the issue");
        }
    }
}