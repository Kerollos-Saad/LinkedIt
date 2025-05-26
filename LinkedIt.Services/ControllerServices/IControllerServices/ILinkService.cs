using LinkedIt.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface ILinkService
	{
		Task<APIResponse> LinkUser(String? linkerId, String? userName);

		Task<APIResponse> UnLinkUser(String? linkerId, String? userName);

		Task<APIResponse> IsLinkingWith(String? linkerId, String? userName);
	}
}
