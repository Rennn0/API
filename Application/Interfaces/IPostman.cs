namespace Application.Interfaces
{
	public interface IPostman
	{
		void Go(string[] addresses, string subject, string body);

		void Go(string address, string subject, string body);
	}
}