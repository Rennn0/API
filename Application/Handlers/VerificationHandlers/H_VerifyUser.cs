using Application.Commands.UserVerificationCommands;
using Application.OtherUtils;
using Domain.DTOs;
using Domain.Entities;
using MediatR;
using Repository.Base;
using Repository.Exceptions;
using static Application.OtherUtils.Security;

namespace Application.Handlers.UserVerificationHandlers
{
	public sealed class H_VerifyUser(IUnitOfWork _unit, Security security) : IRequestHandler<C_VerifyUser, object>
	{
		public async Task<object> Handle(C_VerifyUser request, CancellationToken cancellationToken)
		{
			try
			{
				var record = await _unit.Repository<UserVerification>().FindAsync(x => x.Hash == request.Hash && !x.IsDeleted);
				UserVerification uv = record.FirstOrDefault() ?? throw new NotFoundExc(request.Hash);

				if (uv.CreatedAt + uv.Lifespan < DateTimeOffset.Now.ToUnixTimeMilliseconds())
					return new
					{
						message = "Hash is no longer valid"
					};

				HashPasswordModel hpm = security.CreatePassword(request.Password);

				User user = new User
				{
					Email = uv.Email,
					UserName = uv.UserName,
					Password = hpm.Password,
					Salt = hpm.Salt,
				};

				uv.IsDeleted = true;
				await _unit.Repository<User>().AddAsync(user);
				await _unit.SaveAsync();

				return new UserDto
				{
					Email = user.Email,
					Id = user.Id,
					UserName = user.UserName,
				};
			}
			catch
			{
				_unit.RollBack();
				throw;
			}
		}
	}
}