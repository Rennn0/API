namespace Repository.Exceptions
{
    public class NotFoundExc : Exception
    {
        public NotFoundExc(object resource)
            : base($"Resource {resource} not found")
        { }
    }
}