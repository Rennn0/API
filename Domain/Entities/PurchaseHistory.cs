using Domain.Base;

namespace Domain.Entities
{
	public sealed class PurchaseHistory : EntityBase
	{
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int CreatedDate { get; set; }
		public string Correlation { get; set; }
		public User User { get; set; }
		public Product Product { get; set; }
	}
}