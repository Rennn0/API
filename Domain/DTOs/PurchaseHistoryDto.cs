using Repository.DTOs;

namespace Domain.DTOs
{
	public sealed class PurchaseHistoryDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int CreatedDate { get; set; }
		public string Correlation { get; set; }
		public UserDto? User { get; set; }
		public ProductDto? Product { get; set; }
	}
}