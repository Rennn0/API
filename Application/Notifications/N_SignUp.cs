using MediatR;

namespace Application.Notifications
{
	public sealed class N_SignUp : INotification
	{
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}