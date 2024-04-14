using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.OtherUtils
{
	public static class Security
	{
		private static int _IterationCount = 100000;
		private static int _NumBytes = 32;
		private static readonly RsaSecurityKey _Key;

		static Security()
		{
			var rsa = RSA.Create(2048);
			_Key = new RsaSecurityKey(rsa);
		}

		public static HashPasswordModel CreatePassword(string raw)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(16);
			string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(raw, salt, KeyDerivationPrf.HMACSHA256, _IterationCount, _NumBytes));
			return new HashPasswordModel
			{
				Password = hashedPassword,
				Salt = salt
			};
		}

		public static bool Compare(string raw, string original, byte[] salt)
		{
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(raw, salt, KeyDerivationPrf.HMACSHA256, _IterationCount, _NumBytes));
			return original == hashed;
		}

		public static Token Decode(string token)
		{
			JwtSecurityTokenHandler handler = new();
			TokenValidationParameters decryptingCredentials = new()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _Key,
				ValidateIssuer = false,
				ValidateAudience = false,
				RequireExpirationTime = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
				TokenDecryptionKey = _Key,
				CryptoProviderFactory = new CryptoProviderFactory
				{
					CacheSignatureProviders = false
				}
			};

			ClaimsPrincipal principal = handler.ValidateToken(token, decryptingCredentials, out SecurityToken validatedToken);
			_ = validatedToken as JwtSecurityToken ?? throw new ArgumentException("Invalid Jwt");

			IEnumerable<Claim> claims = principal.Claims;
			Token tokenData = new();
			foreach (var claim in claims)
			{
				switch (claim.Type)
				{
					case "Email":
						tokenData.Email = claim.Value;
						break;

					case "Username":
						tokenData.Username = claim.Value;
						break;

					case "Id":
						tokenData.Id = Convert.ToInt32(claim.Value);
						break;
				}
			}
			return tokenData;
		}

		public static string CreateToken(params (string, string)[] args)
		{
			SigningCredentials credentials = new(_Key, SecurityAlgorithms.RsaSha256);

			List<Claim> claims = [];
			foreach (var claim in args)
			{
				claims.Add(new Claim(claim.Item1, claim.Item2));
			}

			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Subject = new ClaimsIdentity(claims),
				SigningCredentials = credentials,
				Expires = DateTime.UtcNow.AddDays(7)
			};

			JwtSecurityTokenHandler handler = new();
			JwtSecurityToken token = handler.CreateJwtSecurityToken(tokenDescriptor);
			return handler.WriteToken(token);
		}
	}

	public record HashPasswordModel
	{
		public byte[] Salt { get; set; }
		public string Password { get; set; }
	}

	public class Token
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public int Id { get; set; }
	}
}