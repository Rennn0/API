using Application.Commands.VerificationCommands;
using Application.OtherUtils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handlers.VerificationHandlers
{
	public sealed class H_test(IHttpContextAccessor _http) : IRequestHandler<C_Test, object>
	{
		public Task<object> Handle(C_Test request, CancellationToken cancellationToken)
		{
			return Task.FromResult<object>(_http.HttpContext.Items["TokenData"]);
		}
	}
}