using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;

namespace LinkedIt.Services.ControllerServices
{
	public class SignalReactionsService : ISignalReactionsService
	{
		private readonly IUnitOfWork _db;
		public SignalReactionsService(IUnitOfWork db)
		{
			this._db = db;
		}
		public async Task<APIResponse> UpPhantomSignalForUserAsync(String userId, Guid phantomSignalId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var userExist = await _db.User.IsExistAsync(userId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, User Does Not Exist" });
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Signal Does Not Exist" });

			var success = false;
			// toggle Up Vote
			var alreadyUpByUser = await _db.PhantomSignalUp.IsSignalUpByUser(userId, phantomSignalId);
			if (alreadyUpByUser)
			{
				success = await _db.PhantomSignalUp.UpPhantomSignalUndoAsync(userId, phantomSignalId);
				if (!success)
					return APIResponse.Fail(new List<string> { "Failed To UpVote!" });
			}
			else
			{
				success = await _db.PhantomSignalUp.UpPhantomSignalAsync(userId, phantomSignalId);
				if (!success)
					return APIResponse.Fail(new List<string> { "Failed To UpVote!" });
			}

			response.SetResponseInfo(HttpStatusCode.OK, null, new
			{ 
				Status = alreadyUpByUser ? "Remove Up Vote Successfully" : "Added Up Vote Successfully",
				UserId = userId,
				PhantomSignalId = phantomSignalId
			}, true);
			return response;
		}

		public async Task<APIResponse> DownPhantomSignalForUserAsync(string userId, Guid phantomSignalId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var userExist = await _db.User.IsExistAsync(userId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, User Does Not Exist" });
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Signal Does Not Exist" });

			var success = false;
			// toggle Up Vote
			var alreadyDownByUser = await _db.PhantomSignalDown.IsSignalDownByUser(userId, phantomSignalId);
			if (alreadyDownByUser)
			{
				success = await _db.PhantomSignalDown.DownPhantomSignalUndoAsync(userId, phantomSignalId);
				if (!success)
					return APIResponse.Fail(new List<string> { "Failed To DownVote!" });
			}
			else
			{
				success = await _db.PhantomSignalDown.DownPhantomSignalAsync(userId, phantomSignalId);
				if (!success)
					return APIResponse.Fail(new List<string> { "Failed To DownVote!" });
			}

			response.SetResponseInfo(HttpStatusCode.OK, null, new
			{
				Status = alreadyDownByUser ? "Remove Down Vote Successfully" : "Added Down Vote Successfully",
				UserId = userId,
				PhantomSignalId = phantomSignalId
			}, true);
			return response;
		}

		public async Task<APIResponse> PhantomSignalReactionsForUserAsync(Guid phantomSignalId)
		{
			var response = new APIResponse();

			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Signal Does Not Exist" });

			//var ups = await _db.PhantomSignalUp.FindAllAsync(u=>u.PhantomSignalId == phantomSignalId, new []{"ApplicationUser"});
			//var downs = await _db.PhantomSignalDown.FindAllAsync(u => u.PhantomSignalId == phantomSignalId, new []{"ApplicationUser"});
			var upsUserNames = await _db.PhantomSignalUp.UserNamesUpPhantomSignalAsync(phantomSignalId);
			var downsUserNames = await _db.PhantomSignalDown.UserNamesDownPhantomSignalAsync(phantomSignalId);

			response.SetResponseInfo(HttpStatusCode.OK, null, new
			{
				UpList = upsUserNames,
				Downlist = downsUserNames,
				PhantomSignalId = phantomSignalId
			}, true);
			return response;

		}
	}
}
