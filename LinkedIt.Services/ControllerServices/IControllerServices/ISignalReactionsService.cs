using LinkedIt.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface ISignalReactionsService
	{
		Task<APIResponse> UpPhantomSignalForUserAsync(String userId, Guid phantomSignalId);
		Task<APIResponse> DownPhantomSignalForUserAsync(String userId, Guid phantomSignalId);
		Task<APIResponse> PhantomSignalReactionsForUserAsync(Guid phantomSignalId);
	}
}
