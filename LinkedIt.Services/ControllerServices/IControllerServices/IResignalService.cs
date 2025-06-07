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
		Task<APIResponse> GetPhantomReSignalForUserAsync(int phantomResignalId);
		Task<APIResponse> GetPhantomReSignalInDetailsForUserAsync(int phantomResignalId);
		Task<APIResponse> AddPhantomReSignalForUserAsync(String userId, AddResignalDTO addResignalDto);
	}
}
