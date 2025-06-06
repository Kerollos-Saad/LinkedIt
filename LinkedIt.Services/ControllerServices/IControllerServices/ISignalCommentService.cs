using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface ISignalCommentService
	{
		Task<APIResponse> AddCommentPhantomSignalForUserAsync(String userId, Guid phantomSignalId, PhantomSignalCommentDTO phantomSignalCommentDto);
		Task<APIResponse> UpdateCommentPhantomSignalForUserAsync(String userId, int commentId, PhantomSignalCommentDTO phantomSignalCommentDto);
		Task<APIResponse> GetCommentPhantomSignalForUserV1Async(int commentId);
		Task<APIResponse> GetCommentPhantomSignalForUserV2Async (int commentId);
		Task<APIResponse> GetCommentPhantomSignalForUserV3Async (int commentId);

	}
}
