using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;

namespace LinkedIt.Services.ControllerServices
{
	public class SignalCommentService : ISignalCommentService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		public SignalCommentService(IUnitOfWork db, IMapper mapper)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<APIResponse> AddCommentPhantomSignalForUserAsync(string userId, Guid phantomSignalId, 
			PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if(String.IsNullOrEmpty(phantomSignalCommentDto.Comment))
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Comment" });
			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var userExist = await _db.User.IsExistAsync(userId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Signal Does Not Exist" }, HttpStatusCode.NotFound);

			var commentId=
				await _db.PhantomSignalComment.AddCommentPhantomSignalAsync(userId, phantomSignalId,
					phantomSignalCommentDto);

			if(commentId == 0)
				return APIResponse.Fail(new List<string> { "Failed to add your comment" });

			response.SetResponseInfo(HttpStatusCode.Created, null, new { CommentId = commentId }, true);
			return response;
		}

		public async Task<APIResponse> UpdateCommentPhantomSignalForUserAsync(string userId, int commentId,
			PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (String.IsNullOrEmpty(phantomSignalCommentDto.Comment))
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Comment" });

			var userExist = await _db.User.IsExistAsync(userId);
			var commentExist = await _db.PhantomSignalComment.IsExistAsync(commentId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!commentExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Comment Does Not Exist" }, HttpStatusCode.NotFound);

			var isCommentHisProperty = await _db.PhantomSignalComment.IsCommentHisPropertyAsync(userId, commentId);
			if(!isCommentHisProperty)
				return APIResponse.Fail(new List<string>{"UnAuthorize, not Your Comment!"}, HttpStatusCode.Unauthorized);

			var updateSuccess =
				await _db.PhantomSignalComment.UpdateCommentPhantomSignalAsync(userId, commentId,
					phantomSignalCommentDto);

			if (updateSuccess == 0)
				return APIResponse.Fail(new List<string> { "Failed to add your comment" });

			response.SetResponseInfo(HttpStatusCode.Created, null, new { CommentId = commentId }, true);
			return response;
		}

		// Best Performance
		public async Task<APIResponse> GetCommentPhantomSignalForUserV1Async(int commentId)
		{
			var response = new APIResponse();

			var commentExist = await _db.PhantomSignalComment.IsExistAsync(commentId);
			if (!commentExist)
				return APIResponse.Fail(new List<string> { "Comment Does Not Exist" }, HttpStatusCode.NotFound);

			var signalCommentDetailsDto = await _db.PhantomSignalComment.GetCommentPhantomSignalV1Async(commentId);

			response.SetResponseInfo(HttpStatusCode.Created, null, signalCommentDetailsDto, true);
			return response;
		}

		public async Task<APIResponse> GetCommentPhantomSignalForUserV2Async(int commentId)
		{
			var response = new APIResponse();

			var commentExist = await _db.PhantomSignalComment.IsExistAsync(commentId);
			if (!commentExist)
				return APIResponse.Fail(new List<string> { "Comment Does Not Exist" }, HttpStatusCode.NotFound);

			// Without Auto Mapper
			var signalCommentDetailsDto = await _db.PhantomSignalComment.GetCommentPhantomSignalV2Async(commentId);

			response.SetResponseInfo(HttpStatusCode.Created, null, signalCommentDetailsDto, true);
			return response;
		}

		public async Task<APIResponse> GetCommentPhantomSignalForUserV3Async(int commentId)
		{
			var response = new APIResponse();

			var commentExist = await _db.PhantomSignalComment.IsExistAsync(commentId);
			if (!commentExist)
				return APIResponse.Fail(new List<string> { "Comment Does Not Exist" }, HttpStatusCode.NotFound);

			// With Auto Mapper
			var comment = await _db.PhantomSignalComment.FindAsync(c => c.Id == commentId,
				new[] { "ApplicationUser", "PhantomSignal" });

			var signalCommentDetailsDto = _mapper.Map<SignalCommentDetailsDTO>(comment);

			response.SetResponseInfo(HttpStatusCode.Created, null, signalCommentDetailsDto, true);
			return response;
		}

		public async Task<APIResponse> DeletePhantomSignalCommentForUserAsync(string userId, int commentId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			var userExist = await _db.User.IsExistAsync(userId);
			var commentExist = await _db.PhantomSignalComment.IsExistAsync(commentId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!commentExist)
				return APIResponse.Fail(new List<string> { "Comment Does Not Exist" }, HttpStatusCode.NotFound);

			var isCommentHisProperty = await _db.PhantomSignalComment.IsCommentHisPropertyAsync(userId, commentId);
			if (!isCommentHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, not Your Comment!" }, HttpStatusCode.Unauthorized);

			var success = await _db.PhantomSignalComment.DeleteAsync(commentId);

			if (!success)
				return APIResponse.Fail(new List<string> { "Failed to remove your comment" });

			response.SetResponseInfo(HttpStatusCode.Created, null, commentId, true);
			return response;
		}
	}
}
