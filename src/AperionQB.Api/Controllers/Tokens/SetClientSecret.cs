using System;
using AperionQB.Application.Features.QuickBooks.Commands.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
			//Console.WriteLine("\n\n\nid: " + clientInfo[0] + "\nsecret: " + clientInfo[1] + "\nurl: " + clientInfo[2] + "\n\n\n");
			//bool response = true;

			bool response = await _mediator.Send(new UpdateClientInfoCommand(clientInfo[1], clientInfo[0], clientInfo[2]));
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

