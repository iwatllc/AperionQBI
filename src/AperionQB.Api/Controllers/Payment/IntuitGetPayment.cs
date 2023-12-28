using System;
using System.Text.Json;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitGetPayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitGetPayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> getPaymentFromIntuit([FromQuery]int id)
    {
        try
        {
            QBPayment payment = await _mediator.Send(new GetPaymentCommand(id));
            if (payment != null)
            {
                string json = JsonSerializer.Serialize(payment);
                Console.WriteLine(json);
                return Ok(json);
            }
        }catch (Exception e)
        {
            if(e.Message.Contains("Index was out of range."))
            {
                return BadRequest("Looks like this SalesReceipt id doesn't currently exist in QuickBooks or it is negative. Correct this and try again.");
            }

            return BadRequest("An Error occured while getting payment from intuit: " + e.Message + "\n" + e.StackTrace);
        }
        return BadRequest("An Error occured while getting payment from intuit");


    }
}
