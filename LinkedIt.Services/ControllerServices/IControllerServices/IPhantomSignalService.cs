using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IPhantomSignalService
	{
		Task<APIResponse> AddPhantomSignalAsync(String userId, AddPhantomSignalDTO addPhantomSignalDto);
		Task<APIResponse> GetPhantomSignalAsync(String userId, Guid phantomSignalId);
	}
}
