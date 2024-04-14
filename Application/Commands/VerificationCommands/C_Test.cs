using MediatR;

namespace Application.Commands.VerificationCommands
{
	public class C_Test : IRequest<object>
	{
		public string Token { get; set; }
	}
}