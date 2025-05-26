using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;

namespace LinkedIt.Services.ControllerServices
{
	public class LinkService : ILinkService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		public LinkService(IUnitOfWork db, IMapper mapper)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<APIResponse> LinkUser(string? linkerId, string? userName)
		{
			var response = new APIResponse();

			if(linkerId == null)
				return APIResponse.Fail(new List<string>{ "Unauthorized" }, HttpStatusCode.Unauthorized);

			var userToLink = await _db.User.FindAsync(u => u.UserName == userName);

			if (userToLink == null)
				return APIResponse.Fail(new List<string> { "Invalid UserName" });

			if(linkerId == userToLink.Id)
				return APIResponse.Fail(new List<string> { "Really!?. Can't Link With Yourself bro !!." });

			var isAlreadyLinking = await _db.LinkUser.IsAlreadyLinking(linkerId, userToLink.Id);

			if (isAlreadyLinking == true)
				return APIResponse.Fail(new List<string> { "You are already linking with this user." });

			var success = await _db.LinkUser.LinkUser(linkerId, userToLink.Id);
			
			if(!success)
				return APIResponse.Fail(new List<string> { "Failed to link with this user." });

			var linkAdd = new
			{
				Linker = linkerId,
				Linking = userToLink.Id,
				msg = "Link Added Successfully"
			};

			response.SetResponseInfo(HttpStatusCode.OK, null, linkAdd, true);
			return response;
		}

		public async Task<APIResponse> UnLinkUser(string? linkerId, string? userName)
		{
			var response = new APIResponse();

			if (linkerId == null)
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var userToLink = await _db.User.FindAsync(u => u.UserName == userName);

			if (userToLink == null)
				return APIResponse.Fail(new List<string> { "Invalid UserName" });

			if (linkerId == userToLink.Id)
				return APIResponse.Fail(new List<string> { "Really!?. Can't UnLink With Yourself bro !!." });

			var isAlreadyLinking = await _db.LinkUser.IsAlreadyLinking(linkerId, userToLink.Id);

			if (isAlreadyLinking == false)
				return APIResponse.Fail(new List<string> { "You are already unLinking with this user." });

			var success = await _db.LinkUser.UnLinkUser(linkerId, userToLink.Id);

			if (!success)
				return APIResponse.Fail(new List<string> { "Failed to UnLink with this user." });

			var linkRemove = new
			{
				UnLinker = linkerId,
				UnLinking = userToLink.Id,
				msg = "Link Removed Successfully"
			};

			response.SetResponseInfo(HttpStatusCode.OK, null, linkRemove, true);
			return response;
		}
	}
}
