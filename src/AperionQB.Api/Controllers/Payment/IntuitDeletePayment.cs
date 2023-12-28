using System;
using System.Text.Json;
using AperionQB.Application.Features.QuickBooks.Commands;
using AperionQB.Application.Features.QuickBooks.Commands.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntuitDeletePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public IntuitDeletePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> deletePaymentFromIntuit([FromQuery] int id)
    {
        try
        {
            bool response = await _mediator.Send(new DeletePaymentCommand(id));
            if (response)
            {
                return Ok("Payment was deleted from QuickBooks!");
            }
            return BadRequest("Could not delete payment for an unknown reason");
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
