using Application.Interfaces;
using Application.Notifications;
using Application.OtherUtils;
using Application.Templates;
using Domain.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Base;

namespace Application.Handlers.UserVerificationHandlers
{
	public sealed class H_SignUpEmail(IPostman _postman, IOptions<Application.OtherUtils.Domain> _domain, IUnitOfWork _unit) : INotificationHandler<N_SignUp>
	{
		public async Task Handle(N_SignUp notification, CancellationToken cancellationToken)
		{
			string endpoint = "api/v1/user?";
			string domain;
#if DEBUG
			domain = "http://localhost:5246/" + endpoint;
#else
			domain = _domain.Value.Ip + endpoint;
#endif
			string hash = UniqueString.Get();
			string htmlTemplate = Reader.Read("SignUpEmail.html");
			string body = htmlTemplate
				.Replace("{UserName}", notification.UserName)
				.Replace("{Domain}", domain)
				.Replace("{Hash}", hash);

			_postman.Go(notification.Email, "Verify account", body);

			await StoreNewRequest(notification, hash);
		}

		private async Task StoreNewRequest(N_SignUp notification, string hash)
		{
			UserVerification uv = new UserVerification()
			{
				Email = notification.Email,
				Hash = hash,
				Lifespan = 300000, // 5 min
				UserName = notification.UserName
			};
			try
			{
				await _unit.Repository<UserVerification>().AddAsync(uv);
				await _unit.Repository<UserVerification>().SaveAsync();
			}
			catch
			{
				_unit.RollBack();
				throw;
			}
		}
	}
}