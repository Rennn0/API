using Domain.Base;

namespace Domain.Entities
{
	public sealed class UserVerification : VerifyBase
	{
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}