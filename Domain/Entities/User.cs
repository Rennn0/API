using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class User : EntityBase
	{
		public string Name { get; set; }

		public string Email { get; set; }
		public IEnumerable<PurchaseHistory> PurchaseHistories { get; set; }
	}
}