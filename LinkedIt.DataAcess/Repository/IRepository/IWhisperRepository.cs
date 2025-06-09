using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IWhisperRepository : IGenericRepository<Whisper>
	{
		Task<bool> IsWhisperHisPropertyAsync(String userId, Guid whisperId);
		Task<WhisperDetailsDTO> GetWhisperDetailsAsync(Guid whisperId);
		Task<OperationResult<Guid>> AddWhisperWithExistPhantomSignalAsync(String senderId, AddWhisperWithExistPhantomSignalDTO addWhisperDto);
	}
}
