using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.Linker;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.IdentityModel.Tokens;

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

		public async Task<APIResponse> IsLinkingWith(string? linkerId, string? userName)
		{
			var response = new APIResponse();

			if (linkerId == null)
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var userToLink = await _db.User.FindAsync(u => u.UserName == userName);

			if (userToLink == null)
				return APIResponse.Fail(new List<string> { "Invalid UserName" });

			if (linkerId == userToLink.Id)
				return APIResponse.Fail(new List<string> { "Really!?. How is it even possible to link with yourself !!." });

			var isAlreadyLinking = await _db.LinkUser.IsAlreadyLinking(linkerId, userToLink.Id);

			var linkStatus = new
			{
				Linker = linkerId,
				Linking = userToLink.Id,
				IsLinking = isAlreadyLinking
			};

			response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, linkStatus, true);
			return response;
		}

		public async Task<APIResponse> GetMutualLinkersForUserAsync(string? userId, string? targetUserName)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var targetUser = await _db.User.FindAsync(u => u.UserName == targetUserName);

			if (targetUser == null)
				return APIResponse.Fail(new List<string> { "Invalid UserName" });

			if (userId == targetUser.Id)
				return APIResponse.Fail(new List<string> { "How is it even possible to Find Mutual Linkers with yourself Psycho !!." });

			var userLinkTarget = await _db.LinkUser.IsAlreadyLinking(userId, targetUser.Id);
			var targetLinkUser= await _db.LinkUser.IsAlreadyLinking(targetUser.Id, userId);

			if (userLinkTarget == false || targetLinkUser == false)
				return APIResponse.Fail(new List<string> { "Users are not connected mutually !!." });

			var mutualLinkers = await _db.LinkUser.GetMutualLinkersAsync(userId, targetUser.Id);

			var mutualLinkersDto = _mapper.Map<List<LinkerDTO>>(mutualLinkers) ?? new List<LinkerDTO>();

			if (!mutualLinkersDto.Any())
			{
				response.SetResponseInfo(HttpStatusCode.OK, new List<string>() { "There is no Mutual Linkers" }, null,
					true);
				return response;
			}

			var mutualLinkersResponse = new
			{
				Linker = userId,
				Linking = targetUser.Id,
				MutualLinkers = mutualLinkersDto
			};

			response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, mutualLinkersResponse, true);
			return response;
		}

		public async Task<APIResponse> GetLinkersForUserAsync(string? userId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var linkersDto = await _db.LinkUser.GetLinkersDtoAsync(userId);

			if (linkersDto.Count == 0)
			{
				response.SetResponseInfo(HttpStatusCode.OK, new List<string>() { "There is no Linkers for you" }, null,
					true);
				return response;
			}

			var linkersResponse = new
			{
				UserId = userId,
				Linkers = linkersDto
			};

			response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, linkersResponse, true);
			return response;
		}

		public async Task<APIResponse> GetLinkingsForUserAsync(string? userId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var linkingsDto = await _db.LinkUser.GetLinkingsDtoAsync(userId);

			if (linkingsDto.Count == 0)
			{
				response.SetResponseInfo(HttpStatusCode.OK, new List<string>() { "You don't have Linkings with anyone" }, null,
					true);
				return response;
			}

			var linkingsResponse = new
			{
				UserId = userId,
				Linkings = linkingsDto
			};

			response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, linkingsResponse, true);
			return response;
		}

		public async Task<APIResponse> GetLinkersCountForUserAsync(string? userId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var linkersCount = await _db.LinkUser.GetLinkersCount(userId);

			if (linkersCount == 0)
			{
				response.SetResponseInfo(HttpStatusCode.OK, new List<string>() { "There is no Linkers for you" }, null,
					true);
				return response;
			}

			response.SetResponseInfo(HttpStatusCode.OK, null, new
				{
					UserId = userId,
					LinkersNumber = linkersCount,
				},
				true);
			return response;
		}

		public async Task<APIResponse> GetLinkingsCountForUserAsync(string? userId)
		{
			var response = new APIResponse();

			if (String.IsNullOrEmpty(userId))
				return APIResponse.Fail(new List<string> { "Unauthorized" }, HttpStatusCode.Unauthorized);

			var linkingsCount = await _db.LinkUser.GetLinkingsCount(userId);

			if (linkingsCount == 0)
			{
				response.SetResponseInfo(HttpStatusCode.OK, new List<string>() { "You don't have Linkings with anyone" }, null,
					true);
				return response;
			}

			response.SetResponseInfo(HttpStatusCode.OK, null, new
				{
					UserId = userId,
					LinkingsNumber = linkingsCount
				},
				true);
			return response;
		}
	}
}
