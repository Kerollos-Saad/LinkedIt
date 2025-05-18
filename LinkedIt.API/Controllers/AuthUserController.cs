using LinkedIt.Services.ControllerServices.IControllerServices;
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

		[HttpGet("hi")]
		public async Task<IActionResult> Hi()
		{
			return Ok();
		}

	}
}
