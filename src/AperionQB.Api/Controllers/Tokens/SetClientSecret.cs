using System;
using AperionQB.Application.Features.QuickBooks.Commands.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Api.Controllers.Tokens
{
	[Route("api/[controller]")]
	[ApiController]
	public class SetClientSecret : ControllerBase
	{
		private readonly IMediator _mediator;
		public SetClientSecret(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<ActionResult> setClientSecret([FromBody] string[] clientInfo)
		{
			//clientInfo[0] = clientID; clientInfo[1] = clientSecret; clientInfo[2] = callbackURL;
			bool response = await _mediator.Send(new UpdateClientInfoCommand(clientInfo[0], clientInfo[1], clientInfo[2]));
			if (response)
			{
				return Ok("Successfully updated client secret");
			}
			else
			{
				return BadRequest("Unable to update client secret");
			}
		}
	}
}

