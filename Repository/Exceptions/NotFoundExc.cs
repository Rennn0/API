namespace Repository.Exceptions
{
    public class NotFoundExc : Exception
    {
        public NotFoundExc()
        { }

        public NotFoundExc(string message) : base(message)
        {
        }

        public NotFoundExc(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}