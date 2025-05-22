using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;
using LinkedIt.Services.JWTService.IJWTService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkedIt.Services.JWTService
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration _config;
		private readonly string _securityKey;
		private readonly double _tokenDuration;

		public JwtTokenService(IConfiguration config)
		{
			this._config = config;

			//Need To install Package `Microsoft.Extensions.Configuration.Binder` and the method `GetValue` will be available
			_securityKey = _config.GetValue<string>("JWTSettings:Key") ??
			              throw new InvalidOperationException("JWTSettings:Key is not configured.");
			_tokenDuration = _config.GetValue<int?>("JWTSettings:DurationInMinutes") ??
			                throw new InvalidOperationException("JWTSettings:Key is not configured.");
		}

		public string GenerateToken(ApplicationUser user, IList<string> roles)
		{

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_securityKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_tokenDuration),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
