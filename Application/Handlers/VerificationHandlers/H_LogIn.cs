using Application.Commands.VerificationCommands;
using Application.OtherUtils;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Exceptions;
using System.Security.Claims;

namespace Application.Handlers.VerificationHandlers
{
	public sealed class H_LogIn(IUnitOfWork _unit, Security security) : IRequestHandler<C_LogIn, object>
	{
		public async Task<object> Handle(C_LogIn request, CancellationToken cancellationToken)
		{
			User record = await _unit.Repository<User>()
				.QueryBuilder()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken)
				?? throw new NotFoundExc(request.Email);

			if (security.Compare(request.Password, record.Password, record.Salt))
			{
				List<Claim> claims =
				[
					new Claim("Id",record.Id.ToString()),
					new Claim("Username",record.UserName),
					new Claim("Mail",record.Email)
				];

				return new
				{
					Token = security.CreateToken(claims)
				};
			}
			else
			{
				return new
				{
					message = "Wrong password"
				};
			}
		}
	}
}