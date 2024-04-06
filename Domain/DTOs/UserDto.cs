namespace Domain.DTOs
{
	public sealed class UserDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }
		public IEnumerable<PurchaseHistoryDto>? PurchaseHistories { get; set; }
	}
}