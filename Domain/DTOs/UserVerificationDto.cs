namespace Domain.DTOs
{
	public sealed class UserVerificationDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Hash { get; set; }
	}
}