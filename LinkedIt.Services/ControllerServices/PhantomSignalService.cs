using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.IdentityModel.Tokens;

namespace LinkedIt.Services.ControllerServices
{
	public class PhantomSignalService : IPhantomSignalService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;
		public PhantomSignalService(IUnitOfWork db, IMapper mapper)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<APIResponse> AddPhantomSignalAsync(String userId, AddPhantomSignalDTO addPhantomSignalDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(addPhantomSignalDto.SignalContent))
				return APIResponse.Fail(new List<string> { "UnValid Signal!" });

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			var user = await _db.User.FindAsync(u => u.Id == userId);
			
			if(user == null)
				return APIResponse.Fail(new List<string> { "User Not Found" });

			var phantomSignal = _mapper.Map<PhantomSignal>(addPhantomSignalDto);
			phantomSignal.ApplicationUserId = user.Id;

			phantomSignal.SignalDate = DateTime.Now;
			phantomSignal.PhantomFlag = false;

			await _db.PhantomSignal.AddAsync(phantomSignal);
			var success = await _db.SaveAsync();

			if(!success)
				return APIResponse.Fail(new List<string> { "Failed To Add Your Phantom Signal" });

			response.SetResponseInfo(HttpStatusCode.Created, null, new { phantomSignalId = phantomSignal.Id}, true);
			return response;
		}

		public async Task<APIResponse> GetPhantomSignalAsync(String userId, Guid phantomSignalId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			// Use This Stupid Way Only To Figure Out the Auto EF Tracking Entities
			var phantomSignal = await _db.PhantomSignal.FindAsync(s => s.Id == phantomSignalId, asNoTracking: true);
			var user = await _db.User.FindAsync(u => u.Id == userId);

			if (user == null)
				return APIResponse.Fail(new List<string> { "User Not Found" });
			if (phantomSignal == null)
				return APIResponse.Fail(new List<string> { "Phantom Signal Not Found" });

			if (user.Id != phantomSignal.ApplicationUserId)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Not Your Signal!" }, HttpStatusCode.Unauthorized);

			var result = _mapper.Map<PhantomSignalDTO>(phantomSignal);

			response.SetResponseInfo(HttpStatusCode.OK, null, result, true);
			return response;
		}

		public async Task<APIResponse> RemovePhantomSignalAsync(String userId, Guid phantomSignalId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);

			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });

			// To Avoid Tracking Entities 
			var phantomSignalExist = await _db.PhantomSignal.IsSignalExist(phantomSignalId); // Normal Tracking

			if (!phantomSignalExist)
				return APIResponse.Fail(new List<string> { "Phantom Signal Not Found" });

			var isSignalHisProperty = await _db.PhantomSignal.IsSignalHisPropertyAsync(userId, phantomSignalId);
			if(!isSignalHisProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Not Your Signal!" }, HttpStatusCode.Unauthorized);

			var phantomSignal = await _db.PhantomSignal.FindAsync(s => s.Id == phantomSignalId);

			await _db.PhantomSignal.RemoveAsync(phantomSignal);
			var success = await _db.SaveAsync();

			if (!success)
				return APIResponse.Fail(new List<string> { "Failed To Remove!" });

			response.SetResponseInfo(HttpStatusCode.OK, null, phantomSignalId, true);
			return response;
		}

		public async Task<APIResponse> UpdatePhantomSignalForUserAsync(String userId, Guid phantomSignalId, AddPhantomSignalDTO updatePhantomSignalDto)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "UnAuthorize" }, HttpStatusCode.Unauthorized);
			if (phantomSignalId == Guid.Empty)
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Id" });
			if(String.IsNullOrEmpty(updatePhantomSignalDto.SignalContent))
				return APIResponse.Fail(new List<string> { "UnValid Phantom Signal Content" });

			var userExist = await _db.User.IsExistAsync(userId);
			var signalExist = await _db.PhantomSignal.IsExistAsync(phantomSignalId);
			if(!userExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, User Does Not Exist" });
			if(!signalExist)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Signal Does Not Exist" });

			var signalProperty = await _db.PhantomSignal.IsSignalHisPropertyAsync(userId, phantomSignalId);
			if(!signalProperty)
				return APIResponse.Fail(new List<string> { "UnAuthorize, Not Your Signal" }, HttpStatusCode.Unauthorized);

			var success = await _db.PhantomSignal.UpdatePhantomSignalAsync(phantomSignalId, updatePhantomSignalDto);
			if(!success)
				return APIResponse.Fail(new List<string> { "Failed To Update" });

			response.SetResponseInfo(HttpStatusCode.OK, null, phantomSignalId, true);
			return response;
		}
	}
}
