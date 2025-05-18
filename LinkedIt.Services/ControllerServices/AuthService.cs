using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Identity;

namespace LinkedIt.Services.ControllerServices
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;

		private APIResponse response;

		public AuthService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
			this._userManager = userManager;
			response = new APIResponse();
		}

		public async Task<APIResponse> LoginAsync(LoginRequestDTO loginRequestDTO)
		{
			var responseLogin = await _unitOfWork.User.Login(loginRequestDTO);
			if (responseLogin == null || string.IsNullOrEmpty(responseLogin.Token))
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest,
					new List<string> { "Username or password is incorrect" }, null, false);
				return response;
			}
			response.SetResponseInfo(HttpStatusCode.OK, null, responseLogin, true);
			return response;
		}

		public async Task<APIResponse> RegisterAsync(ApplicationUserToAddUserDTO registerDTO)
		{
			var response = new APIResponse();

			// Validate Request Data
			if (registerDTO == null)
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Registration data is null" },
					null, false);
				return response;
			}

			// Unique User Name
			bool isUnique = await _unitOfWork.User.IsUniqueUserName(registerDTO.UserName);
			if (!isUnique)
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Registration data is null" },
					null, false);
				return response;
			}

			var user = _mapper.Map<ApplicationUser>(registerDTO);



			try
			{
				var result = await _userManager.CreateAsync(user, registerDTO.Password);

				if (!result.Succeeded)
				{
					response.SetResponseInfo(HttpStatusCode.BadRequest,
						result.Errors.Select(e => e.Description).ToList(),
						null, false);
					return response;
				}

				response.SetResponseInfo(HttpStatusCode.Created, null, registerDTO, true);
				return response;
			}
			catch (Exception ex)
			{
				response.SetResponseInfo(HttpStatusCode.InternalServerError, new List<string> { "An error occurred while registering the user.", ex.Message },
					null, false);
				return response;
			}
		}
	}
}
