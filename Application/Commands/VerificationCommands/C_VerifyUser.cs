using Domain.DTOs;
using MediatR;

namespace Application.Commands.UserVerificationCommands
{
	public sealed class C_VerifyUser : IRequest<object>
	{
		public string Hash { get; set; }
		public string Password { get; set; }
	}
}