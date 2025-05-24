using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using LinkedIt.Services.JWTService.IJWTService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LinkedIt.Services.ControllerServices
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly ILogger<AuthService> _logger;

		public AuthService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService, ILogger<AuthService> logger)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
			this._userManager = userManager;
			this._jwtTokenService = jwtTokenService;
			this._logger = logger;
		}

		public async Task<APIResponse> LoginAsync(LoginRequestDTO loginRequestDTO)
		{
			APIResponse response = new APIResponse();

			var userWithRoles = await _unitOfWork.User.GetUserWithRoles(loginRequestDTO.UserName, loginRequestDTO.Password);

			if (userWithRoles == null)
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest,
					new List<string> { "Username or password is incorrect" }, null, false);
				return response;
			}

			// Generate JWT Token Here

			var token = _jwtTokenService.GenerateToken(userWithRoles.User, userWithRoles.Roles);

			response.SetResponseInfo(
				HttpStatusCode.OK,
				null,
				result: new LoginResponseDTO
				{
					Token = token,
					User = _mapper.Map<UserDTO>(userWithRoles.User)
				},
				true);
			return response;
		}

		public async Task<APIResponse> RegisterAsync(ApplicationUserToAddUserDTO registerDTO)
		{
			APIResponse response = new APIResponse();

			// Validate Request Data
			if (registerDTO == null)
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Registration data is null." },
					null, false);
				return response;
			}

			// Unique User Name
			bool isUnique = await _unitOfWork.User.IsUniqueUserName(registerDTO.UserName);
			if (!isUnique)
			{
				response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "User Name Was Taken!." },
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
				_logger.LogError(ex, "An error occurred while registering the user.");
				response.SetResponseInfo(HttpStatusCode.InternalServerError, new List<string> { "An error occurred while registering the user.", ex.Message },
					null, false);
				return response;
			}
		}
	}
}
