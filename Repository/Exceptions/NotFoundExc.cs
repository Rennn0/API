namespace Repository.Exceptions
{
	[Serializable]
	public class NotFoundExc : Exception
	{
		public NotFoundExc(object resource)
			: base($"Resource {resource} not found")
		{ }
	}
}