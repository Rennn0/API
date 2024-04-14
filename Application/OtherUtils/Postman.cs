using Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Application.OtherUtils
{
	public class Postman : IPostman
	{
		private readonly ISmtp _smtp;
		private readonly SmtpClient _client;
		private readonly NetworkCredential _credentials;

		public Postman(IOptions<Smtp> smtp)
		{
			_smtp = smtp.Value;
			_credentials = new NetworkCredential(_smtp.Username, _smtp.Password);
			_client = new SmtpClient(_smtp.Host, _smtp.Port)
			{
				EnableSsl = true,
				Credentials = _credentials
			};
		}

		public void Go(string[] addresses, string subject, string body)
		{
			MailMessage message = GetMessage(subject, body);

			foreach (string address in addresses)
				message.To.Add(address);

			if (message.To.Count == 0)
				return;

			_client.SendMailAsync(message);
		}

		public void Go(string address, string subject, string body)
		{
			MailMessage message = GetMessage(subject, body);
			message.To.Add(address);

			_client.SendMailAsync(message);
		}

		private MailMessage GetMessage(string subject, string body)
		{
			return new MailMessage
			{
				From = new MailAddress(_smtp.From, _smtp.DisplayName, System.Text.Encoding.UTF8),
				Body = body,
				IsBodyHtml = true,
				Subject = subject,
			};
		}
	}
}