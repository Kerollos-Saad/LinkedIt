using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.Models.Phantom_Signal;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IPhantomSignalCommentRepository : IGenericRepository<PhantomSignalComment>
	{
		Task<int> AddCommentPhantomSignalAsync(String userId, Guid phantomSignalId, PhantomSignalCommentDTO phantomSignalCommentDto);
		Task<int> UpdateCommentPhantomSignalAsync(String userId, int commentId, PhantomSignalCommentDTO phantomSignalCommentDto);
		Task<bool> IsCommentHisPropertyAsync(String userId, int commentId);
		Task<SignalCommentDetailsDTO> GetCommentPhantomSignalV1Async(int commentId);
		Task<SignalCommentDetailsDTO> GetCommentPhantomSignalV2Async(int commentId);
		Task<bool> DeleteAsync(int commentId);
	}
}
