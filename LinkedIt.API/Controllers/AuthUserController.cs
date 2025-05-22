using System.Net;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthUserController : ControllerBase
	{

		private readonly IAuthService _authService;

		public AuthUserController(IAuthService authService)
		{
			this._authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = await _authService.LoginAsync(loginRequestDto);

			if (response.IsSuccess)
				return Ok(response);
			
			return BadRequest(response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] ApplicationUserToAddUserDTO registerRequestDto)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = await _authService.RegisterAsync(registerRequestDto);

			if (response.IsSuccess)
				return Ok(response);

			if (response.StatusCode == HttpStatusCode.InternalServerError)
				return StatusCode(500, response);

			return BadRequest(response);
		}

		[HttpGet("JwtTester")]
		[Authorize]
		public IActionResult tokenTester()
		{
			return Ok("Hi ");
		}

	}
}
