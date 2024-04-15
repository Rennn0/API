using Application.Commands.VerificationCommands;
using Application.OtherUtils;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Handlers.VerificationHandlers
{
	public sealed class H_test(IHttpContextAccessor _http) : IRequestHandler<C_Test, List<ClaimDto>>
	{
		public Task<List<ClaimDto>> Handle(C_Test request, CancellationToken cancellationToken)
		{
			ClaimsIdentity iden = _http.HttpContext.User.Identity as ClaimsIdentity ?? throw new ArgumentException();

			IEnumerable<Claim> claims = iden.Claims;
			return Task.FromResult(claims.Select(x => new ClaimDto
			{
				Type = x.Type,
				Value = x.Value,
			}).ToList());
		}
	}

	public class ClaimDto
	{
		public string Type { get; set; }
		public string Value { get; set; }
	}
}