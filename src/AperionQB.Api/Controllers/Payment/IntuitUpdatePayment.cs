using System;
using System.Text.Json;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitUpdatePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitUpdatePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePaymentOnIntuit([FromQuery] string json)
    {
        QBPayment payment;
        try
        {
            payment = JsonConvert.DeserializeObject<QBPayment>(json);
        }
        catch (Exception e)
        {
            return BadRequest("" +
                "Could not convert from json. Please check formatting and try again: " + e.Message);
        }

        try
        {
            bool result = await _mediator.Send(new UpdatePaymentCommand(payment));
            if (result)
            {
                return Ok("Successfully updated payment in intuit");
            }
            else
            {
                return BadRequest("Update failed for an unknown reason");
            }
            
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Index was out of range."))
            {
                return BadRequest("Looks like this SalesReceipt id doesn't currently exist in QuickBooks or it is negative. Correct this and try again.");
            }

            return BadRequest("An Error occured while getting payment from intuit: " + e.Message + "\n" + e.StackTrace);
        }
        return BadRequest("An Error occured while getting payment from intuit");


    }
}
