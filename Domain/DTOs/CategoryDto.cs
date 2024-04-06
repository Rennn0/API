namespace Repository.DTOs
{
	public sealed class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<ProductDto>? Products { get; set; }
	}
}