using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IWhisperService
	{
		Task<APIResponse> GetWhisperForUserAsync(String userId, Guid whisperId);
		Task<APIResponse> GetWhisperDetailsForUserAsync(String userId, Guid whisperId);

		Task<APIResponse> AddWhisperWithExistPhantomSignalForUserAsync(String senderId,
			AddWhisperWithExistPhantomSignalDTO addWhisperDto);

		Task<APIResponse> AddWhisperWithNewPhantomSignalForUserAsync(String senderId,
			AddWhisperWithNewPhantomSignalDTO addWhisperDto);

		APIResponse GetWhisperStatusOptions();

		Task<APIResponse> UpdateWhisperStatusForUserAsync(String receiverId, Guid whisperId,
			WhisperStatusUpdateDTO whisperStatusUpdateDto);

		Task<APIResponse> RemoveWhisperForUserAsync(String userId, Guid whisperId);
	}
}
