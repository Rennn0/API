namespace Domain.Interfaces
{
	public interface IEntity
	{
		int Id { get; set; }
		long CreatedAt { get; set; }
		long ModifiedAt { get; set; }
		bool IsDeleted { get; set; }
	}
}