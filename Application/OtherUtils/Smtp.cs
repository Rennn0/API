using Application.Interfaces;

namespace Application.OtherUtils
{
	public sealed class Smtp : ISmtp
	{
		public string Host { get; set; }
		public string DisplayName { get; set; }
		public string From { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int Port { get; set; }
	}
}