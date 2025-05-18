using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IAuthService
	{
		Task<APIResponse> LoginAsync(LoginRequestDTO loginRequestDTO);
		Task<APIResponse> RegisterAsync(ApplicationUserToAddUserDTO registerRequestDTO);
	}
}
