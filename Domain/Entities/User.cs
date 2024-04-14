using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class User : EntityBase
	{
		public string UserName { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }
		public byte[] Salt { get; set; }
		public IEnumerable<PurchaseHistory> PurchaseHistories { get; set; }
	}
}