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
		Task<APIResponse> LinkUserAsync(String? linkerId, String? userName);

		Task<APIResponse> UnLinkUserAsync(String? linkerId, String? userName);

		Task<APIResponse> IsLinkingWithAsync(String? linkerId, String? userName);

		Task<APIResponse> GetMutualLinkersForUserAsync(String? userId, String? targetUserName);

		Task<APIResponse> GetLinkersForUserAsync(String? userId);

		Task<APIResponse> GetLinkingsForUserAsync(String? userId);

		Task<APIResponse> GetLinkersCountForUserAsync(String? userId);

		Task<APIResponse> GetLinkingsCountForUserAsync(String? userId);

		Task<APIResponse> GetLinkers_LinkingsCountForUserAsync(String? userId);
	}
}
