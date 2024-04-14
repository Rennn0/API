using Application.Commands.VerificationCommands;
using Application.Handlers.UserVerificationHandlers;
using Application.OtherUtils;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Base;
using Repository.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Handlers.VerificationHandlers
{
	public sealed class H_LogIn(IUnitOfWork _unit, IOptions<JwtConfig> _jwt) : IRequestHandler<C_LogIn, object>
	{
		public async Task<object> Handle(C_LogIn request, CancellationToken cancellationToken)
		{
			User record = await _unit.Repository<User>()
				.QueryBuilder()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken)
				?? throw new NotFoundExc(request.Email);

			if (Security.Compare(request.Password, record.Password, record.Salt))
			{
				SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwt.Value.Key));
				SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

				List<Claim> claims =
				[
					new Claim("id",record.Id.ToString()),
					new Claim("username",record.UserName),
					new Claim("email",record.Email)
				];
				JwtSecurityToken secToken = new(
					issuer: _jwt.Value.Issuer,
					audience: _jwt.Value.Audience,
					claims: claims,
					expires: DateTime.UtcNow.AddSeconds(30),
					notBefore: DateTime.UtcNow,
					signingCredentials: credentials);

				string token = new JwtSecurityTokenHandler().WriteToken(secToken);
				return new
				{
					Token = token
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