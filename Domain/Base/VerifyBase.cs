namespace Domain.Base
{
	public class VerifyBase : EntityBase
	{
		public string Hash { get; set; }
		public long Lifespan { get; set; } // milliseconds
	}
}