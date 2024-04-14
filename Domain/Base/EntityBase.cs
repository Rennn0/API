using Domain.Interfaces;

namespace Domain.Base
{
	public class EntityBase : IEntity
	{
		public int Id { get; set; }
		public long CreatedAt { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		public long ModifiedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}