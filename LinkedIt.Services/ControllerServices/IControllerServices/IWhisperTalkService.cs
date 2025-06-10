using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IWhisperTalkService
	{
		Task<APIResponse> GetWhisperTalksForUserAsync(String userId, Guid whisperId);
		Task<APIResponse> AddWhisperTalkForUserAsync(String senderId, Guid whisperId, AddWhisperTalkDTO whisperTalkDto);
	}
}
