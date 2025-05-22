using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;

namespace LinkedIt.Services.JWTService.IJWTService
{
	public interface IJwtTokenService
	{
		string GenerateToken(ApplicationUser user, IList<String> roles);
	}
}
