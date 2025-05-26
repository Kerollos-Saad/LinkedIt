using System.Net;
using System.Security.Claims;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class LinkUserController : ControllerBase
	{
		private readonly ILinkService _linkService;
		public LinkUserController(ILinkService linkService)
		{
			this._linkService = linkService;
		}

		[HttpPost("LinkUser")]
		public async Task<IActionResult> LinkUser(String userName)
		{
			var linkerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.LinkUser(linkerId, userName);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);
		}
	}
}
