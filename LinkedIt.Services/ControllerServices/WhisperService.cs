using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.Whisper;

namespace LinkedIt.Services.ControllerServices
{
	public class WhisperService : IWhisperService
	{
		private readonly IUnitOfWork _db;

		public WhisperService(IUnitOfWork db)
		{
			this._db = db;
		}

		public async Task<APIResponse> GetWhisperForUserAsync(string userId, Guid whisperId)
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

			var isWhisperHisProperty = await _db.Whisper.IsWhisperHisPropertyAsync(userId, whisperId);
			if(!isWhisperHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Your Are not part of this Whisper" }, HttpStatusCode.Unauthorized);

			var whisper = await _db.Whisper.FindAsync(w => w.Id == whisperId);

			response.SetResponseInfo(HttpStatusCode.OK, null, whisper, true);
			return response;
		}

		public async Task<APIResponse> GetWhisperDetailsForUserAsync(string userId, Guid whisperId)
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

			var isWhisperHisProperty = await _db.Whisper.IsWhisperHisPropertyAsync(userId, whisperId);
			if (!isWhisperHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Your Are not part of this Whisper" }, HttpStatusCode.Unauthorized);

			var whisperDetailsDto = await _db.Whisper.GetWhisperDetailsAsync(whisperId);

			response.SetResponseInfo(HttpStatusCode.OK, null, whisperDetailsDto, true);
			return response;
		}

		public async Task<APIResponse> AddWhisperWithExistPhantomSignalForUserAsync(string senderId,
			AddWhisperWithExistPhantomSignalDTO addWhisperDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(senderId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (addWhisperDto.phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var userExist = await _db.User.IsExistAsync(senderId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(addWhisperDto.phantomSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "Phantom Signal Does Not Exist" }, HttpStatusCode.NotFound);

			// Check Users Are Connected
			var usersAreLinked =
				await _db.LinkUser.IsAlreadyLinking(senderId, addWhisperDto.ReceiverId)
				&&
				await _db.LinkUser.IsAlreadyLinking(addWhisperDto.ReceiverId, senderId);

			if(!usersAreLinked)
				return APIResponse.Fail(new List<string> { "You Can't Send Whisper To This User, Not linked" }, HttpStatusCode.Unauthorized);

			var result = await _db.Whisper.AddWhisperWithExistPhantomSignalAsync(senderId, addWhisperDto);
			if (!result.IsSuccess)
				return APIResponse.Fail(new List<string> { $"{result.ErrorMessage}" });

			response.SetResponseInfo(HttpStatusCode.Created, null, result.Data, true);
			return response;
		}
	}
}
