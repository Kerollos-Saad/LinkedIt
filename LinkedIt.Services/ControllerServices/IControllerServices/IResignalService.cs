using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IResignalService
	{
		Task<APIResponse> GetPhantomReSignalForUserAsync(int resignalId);
		Task<APIResponse> GetPhantomReSignalInDetailsForUserAsync(int resignalId);
		Task<APIResponse> AddPhantomReSignalForUserAsync(String userId, Guid phantomSignalId, AddResignalDTO addResignalDto);
		Task<APIResponse> UpdatePhantomReSignalForUserAsync(String userId, int reSignalId, AddResignalDTO updateResignalDto);
		Task<APIResponse> DeletePhantomReSignalForUserAsync(String userId, int reSignalId);
	}
}
