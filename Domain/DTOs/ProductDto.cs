﻿namespace Repository.DTOs
{
	public sealed class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public CategoryDto? Category { get; set; }
	}
}