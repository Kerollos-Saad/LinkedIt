using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Services.ControllerServices.IControllerServices;
using LinkedIt.Services.ControllerServices;
using LinkedIt.Services.JWTService.IJWTService;
using LinkedIt.Services.JWTService;
using Microsoft.Extensions.DependencyInjection;

namespace LinkedIt.Services.ServiceRegistration
{
	public static class ServiceRegistration
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IJwtTokenService, JwtTokenService>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ILinkService, LinkService>();
		}
	}
}
