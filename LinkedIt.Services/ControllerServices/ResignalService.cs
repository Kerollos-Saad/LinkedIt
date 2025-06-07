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
	public class ResignalService : IResignalService
	{
		private readonly IUnitOfWork _db;
		public ResignalService(IUnitOfWork db)
		{
			this._db = db;
		}

		public async Task<APIResponse> GetPhantomReSignalForUserAsync(int phantomResignalId)
		{
			var response = new APIResponse();

			var reSignal = await _db.PhantomResignal.FindAsync(r => r.Id == phantomResignalId);

			if (reSignal == null)
				return APIResponse.Fail(new List<string>{"ReSignal Not Found!"}, HttpStatusCode.NotFound);

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignal, true);
			return response;
		}

		public async Task<APIResponse> GetPhantomReSignalInDetailsForUserAsync(int phantomResignalId)
		{
			var response = new APIResponse();

			var reSignalExist = await _db.PhantomResignal.IsExistAsync(phantomResignalId);
			if(!reSignalExist)
				return APIResponse.Fail(new List<string> { "ReSignal Not Found!" }, HttpStatusCode.NotFound);

			var reSignalDetailsDTO = await _db.PhantomResignal.GetPhantomReSignalInDetailsAsync(phantomResignalId);

			response.SetResponseInfo(HttpStatusCode.OK, null, reSignalDetailsDTO, true);
			return response;
		}
	}
}
