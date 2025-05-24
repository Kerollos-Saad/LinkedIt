using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.Enums;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;

namespace LinkedIt.Services.ControllerServices
{
	public class UserService : IUserService
	{

		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		private readonly APIResponse _response;

		public UserService(IUnitOfWork db, IMapper mapper)
		{
			this._db = db;
			this._mapper = mapper;
			_response = new APIResponse();
		}

		public async Task<APIResponse> GetUserProfileAsync(Expression<Func<ApplicationUser, bool>> filter,
			UserProfileQueryType queryType,
			string[]? includeProperties = null)
		{
			var userProfile = await _db.User.FindAsync(filter, includeProperties);

			if (userProfile == null)
			{
				switch (queryType)
				{
					case UserProfileQueryType.ById:
						_response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
						break;
					case UserProfileQueryType.ByUsername:
						_response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "No user found with this username." }, null, false);
						break;
				}
				return _response;
			}

			ApplicationUserDTO userProfileDto = _mapper.Map<ApplicationUserDTO>(userProfile);

			_response.SetResponseInfo(HttpStatusCode.Found, null, userProfileDto, true);
			return _response;
		}
	}
}
