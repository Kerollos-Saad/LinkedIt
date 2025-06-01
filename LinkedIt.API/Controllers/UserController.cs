using System.Net;
using System.Security.Claims;
using AutoMapper;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Enums;
using LinkedIt.Core.Response;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			this._userService = userService;
		}

		[HttpGet("userProfile")]
		public async Task<IActionResult> GetUserProfile(string userName)
		{
			var response = await _userService.GetUserProfileAsync(x => x.UserName == userName, UserProfileQueryType.ByUsername);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.NotFound)
				return NotFound(response);

			return Ok(response);
		}

		[HttpGet()]
		public async Task<IActionResult> GetMyProfile()
		{
			var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _userService.GetUserProfileAsync(x => x.Id == id, UserProfileQueryType.ById);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.NotFound)
				return NotFound(response);

			return Ok(response);
		}

		// Work With User Id cause only User Can Update Or Remove His Profile
		[HttpPut()]
		public async Task<IActionResult> UpdateUserProfile(UpdateUserDTO userDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _userService.UpdateUserProfileAsync(id, userDto);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);

		}

		[HttpDelete()]
		public async Task<IActionResult> DeleteUser()
		{
			var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _userService.DeleteUserProfileAsync(id);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);
		}
	}
}
