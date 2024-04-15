using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.OtherUtils
{
	public class Security
	{
		private readonly int _IterationCount = 100000;
		private readonly int _NumBytes = 32;
		private readonly RsaSecurityKey _Key;
		private readonly JwtConfig _jwt;

		public Security(IOptions<JwtConfig> jwt)
		{
			var rsa = RSA.Create(2048);
			_Key = new RsaSecurityKey(rsa);
			_jwt = jwt.Value;
		}

		public HashPasswordModel CreatePassword(string raw)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(16);
			string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(raw, salt, KeyDerivationPrf.HMACSHA256, _IterationCount, _NumBytes));
			return new HashPasswordModel
			{
				Password = hashedPassword,
				Salt = salt
			};
		}

		public bool Compare(string raw, string original, byte[] salt)
		{
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(raw, salt, KeyDerivationPrf.HMACSHA256, _IterationCount, _NumBytes));
			return original == hashed;
		}

		public IEnumerable<Claim> DecodeSecured(string token)
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

			return claims;
		}

		public string CreateSecuredToken(params (string, string)[] args)
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

		public string CreateToken(IEnumerable<Claim> claims)
		{
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwt.Key));
			SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken secToken = new(
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddHours(1),
				notBefore: DateTime.UtcNow,
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(secToken);
		}

		public record HashPasswordModel
		{
			public byte[] Salt { get; set; }
			public string Password { get; set; }
		}
	}
}