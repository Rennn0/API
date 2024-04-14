using Application.Commands.UserVerificationCommands;
using Application.Commands.VerificationCommands;
using Application.Notifications;
using Application.OtherUtils;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiVersion(1)]
	[ApiVersion(2)]
	[Route("api/v{v:apiVersion}/[controller]")]
	[ApiController]
	public sealed class VerificationController(IMediator _mediator) : ControllerBase
	{
		[AllowAnonymous]
		[HttpPost("SignUp")]
		public Task SignUp([FromBody] N_SignUp notification)
		{
			return _mediator.Publish(notification);
		}

		[AllowAnonymous]
		[HttpPost("Verify/user")]
		public async Task<object> Verify([FromBody] C_VerifyUser command)
		{
			return await _mediator.Send(command);
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<object> Login([FromBody] C_LogIn command)
		{
			return await _mediator.Send(command);
		}

		[HttpPost]
		public async Task<object> Test([FromBody] C_Test command)
		{
			return await _mediator.Send(command);
		}
	}
}