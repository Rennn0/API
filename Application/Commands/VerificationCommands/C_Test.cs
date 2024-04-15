using Application.Handlers.VerificationHandlers;
using MediatR;
using System.Security.Claims;

namespace Application.Commands.VerificationCommands
{
	public class C_Test : IRequest<List<ClaimDto>>
	{
		public string Token { get; set; }
	}
}