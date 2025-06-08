using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Services.ControllerServices
{
	public class ResignalService : IResignalService
	{
		private readonly IUnitOfWork _db;
		public ResignalService(IUnitOfWork db)
		{
			this._db = db;
		}

		public async Task<APIResponse> GetPhantomReSignalForUserAsync(int resignalId)
		{
			var response = new APIResponse();

			var reSignal = await _db.PhantomResignal.FindAsync(r => r.Id == resignalId);

			if (reSignal == null)
				return APIResponse.Fail(new List<string>{"ReSignal Not Found!"}, HttpStatusCode.NotFound);

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignal, true);
			return response;
		}

		public async Task<APIResponse> GetPhantomReSignalInDetailsForUserAsync(int resignalId)
		{
			var response = new APIResponse();

			var reSignalExist = await _db.PhantomResignal.IsExistAsync(resignalId);
			if(!reSignalExist)
				return APIResponse.Fail(new List<string> { "ReSignal Not Found!" }, HttpStatusCode.NotFound);

			var reSignalDetailsDTO = await _db.PhantomResignal.GetPhantomReSignalInDetailsAsync(resignalId);

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignalDetailsDTO, true);
			return response;
		}

		public async Task<APIResponse> AddPhantomReSignalForUserAsync(string userId, Guid phantomSignalId, AddResignalDTO addResignalDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			var userExist = await _db.User.IsExistAsync(userId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!signalExist)
				return APIResponse.Fail(new List<string> { "Signal Does Not Exist" }, HttpStatusCode.NotFound);

			var reSignalId = await _db.PhantomResignal.AddPhantomReSignalAsync(userId, phantomSignalId, addResignalDto);
			if(reSignalId == 0)
				return APIResponse.Fail(new List<string> { "Failed To ReSignal This Signal!" });

			response.SetResponseInfo(HttpStatusCode.Created, null, reSignalId, true);
			return response;
		}

		public async Task<APIResponse> UpdatePhantomReSignalForUserAsync(string userId, int reSignalId, AddResignalDTO updateResignalDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			var userExist = await _db.User.IsExistAsync(userId);
			var reSignalExist = await _db.PhantomResignal.IsExistAsync(reSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!reSignalExist)
				return APIResponse.Fail(new List<string> { "Signal Does Not Exist" }, HttpStatusCode.NotFound);

			var isReSignalHisProperty = await _db.PhantomResignal.IsResignalHisPropertyAsync(userId, reSignalId);
			if (!isReSignalHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, not Your ReSignal!" }, HttpStatusCode.Unauthorized);

			var success = await _db.PhantomResignal.UpdatePhantomReSignalAsync(reSignalId, updateResignalDto);
			if(!success)
				return APIResponse.Fail(new List<string> { "Failed To Update ReSignal!" });

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignalId, true);
			return response;
		}

		public async Task<APIResponse> DeletePhantomReSignalForUserAsync(string userId, int reSignalId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			var userExist = await _db.User.IsExistAsync(userId);
			var reSignalExist = await _db.PhantomResignal.IsExistAsync(reSignalId);
			if (!userExist)
				return APIResponse.Fail(new List<string> { "User Does Not Exist" }, HttpStatusCode.NotFound);
			if (!reSignalExist)
				return APIResponse.Fail(new List<string> { "ReSignal Does Not Exist" }, HttpStatusCode.NotFound);

			var isReSignalHisProperty = await _db.PhantomResignal.IsResignalHisPropertyAsync(userId, reSignalId);
			if (!isReSignalHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, not Your ReSignal!" }, HttpStatusCode.Unauthorized);

			var success = await _db.PhantomResignal.DeletePhantomReSignalAsync(reSignalId);
			if(!success)
				return APIResponse.Fail(new List<string> { "Failed To Delete ReSignal!" });

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignalId, true);
			return response;
		}
	}
}
