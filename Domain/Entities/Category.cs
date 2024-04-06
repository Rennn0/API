using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class Category : EntityBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}