using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IWhisperTalkRepository : IGenericRepository<WhisperTalk>
	{
		Task<OperationResult<ICollection<WhisperTalkDetailsDTO>>> GetWhisperTalksAsync(Guid whisperId);
		Task<OperationResult<int>> AddWhisperTalkAsync(String senderId, Guid whisperId, AddWhisperTalkDTO addWhisperTalkDto);
	}
}
