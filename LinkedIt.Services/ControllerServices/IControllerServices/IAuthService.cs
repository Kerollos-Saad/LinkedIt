using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.Authentication;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IAuthService
	{
		Task<object> LoginAsync(LoginRequestDTO loginRequestDTO);
		Task<object> RegisterAsync(RegisterRequestDTO registerRequestDTO);
	}
}
