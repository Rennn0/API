using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class Product : EntityBase
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public IEnumerable<PurchaseHistory> PurchaseHistories { get; set; }
	}
}