using MediatR;

namespace Application.Commands.VerificationCommands
{
	public sealed class C_LogIn : IRequest<object>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}