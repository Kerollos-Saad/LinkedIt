using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Services.ControllerServices
{
	public class WhisperTalkService : IWhisperTalkService
	{
		private readonly IUnitOfWork _db;
		public WhisperTalkService(IUnitOfWork db)
		{
			this._db = db;
		}

		public async Task<APIResponse> GetWhisperTalksForUserAsync(string userId, Guid whisperId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (whisperId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Whisper Id" });

			var userExist = await _db.User.IsExistAsync(userId);
			var whisperExist = await _db.Whisper.IsExistAsync(whisperId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!whisperExist)
				return APIResponse.Fail(new List<string> { "Whisper Does Not Exist" }, HttpStatusCode.NotFound);

			var userIsPartOfWhisper = await _db.Whisper.IsWhisperHisPropertyAsync(userId, whisperId);
			if (!userIsPartOfWhisper)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Your Are not part of this Whisper" }, HttpStatusCode.Unauthorized);

			var result = await _db.WhisperTalk.GetWhisperTalksAsync(whisperId);
			if (!result.IsSuccess)
				return APIResponse.Fail(new List<string> { $"{result.ErrorMessage}" });

			response.SetResponseInfo(HttpStatusCode.OK, null, result.Data, true);
			return response;
		}

		public async Task<APIResponse> AddWhisperTalkForUserAsync(string senderId, Guid whisperId, AddWhisperTalkDTO whisperTalkDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(senderId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (whisperId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Whisper Id" });

			var userExist = await _db.User.IsExistAsync(senderId);
			var whisperExist = await _db.Whisper.IsExistAsync(whisperId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!whisperExist)
				return APIResponse.Fail(new List<string> { "Whisper Does Not Exist" }, HttpStatusCode.NotFound);

			var userIsPartOfWhisper = await _db.Whisper.IsWhisperHisPropertyAsync(senderId, whisperId);
			if (!userIsPartOfWhisper)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Your Are not part of this Whisper" }, HttpStatusCode.Unauthorized);

			var result = await _db.WhisperTalk.AddWhisperTalkAsync(senderId, whisperId, whisperTalkDto);
			if (!result.IsSuccess)
				return APIResponse.Fail(new List<string> { $"{result.ErrorMessage}" });

			response.SetResponseInfo(HttpStatusCode.OK, null, new { TalkId = result.Data }, true);
			return response;
		}

		public async Task<APIResponse> RemoveWhisperTalkForUserAsync(string userId, int talkId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			var userExist = await _db.User.IsExistAsync(userId);
			var talkExist = await _db.WhisperTalk.IsExistAsync(talkId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!talkExist)
				return APIResponse.Fail(new List<string> { "Whisper Talk Does Not Exist" }, HttpStatusCode.NotFound);

			var isTalkHisProperty = await _db.WhisperTalk.IsExistAsync(t => t.Id == talkId && t.SenderId == userId);
			if (!isTalkHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Not Your Talk" }, HttpStatusCode.Unauthorized);

			var isWithinAllowedPeriod = await _db.WhisperTalk.IsExistAsync(t=>t.Id == talkId && DateTime.Now < t.TalkDate.AddHours(24));
			if(!isWithinAllowedPeriod)
				return APIResponse.Fail(new List<string> { "Can't Remove Talk After 24 hours." });

			var result = await _db.WhisperTalk.RemoveWhisperTalkAsync(talkId);
			if (!result.IsSuccess)
				return APIResponse.Fail(new List<string> { $"{result.ErrorMessage}" });

			response.SetResponseInfo(HttpStatusCode.OK, null, new { Success = result.Data }, true);
			return response;
		}
	}
}
