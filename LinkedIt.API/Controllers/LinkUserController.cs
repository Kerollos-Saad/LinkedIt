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

		[HttpPost("{userName}")]
		public async Task<IActionResult> LinkUser([FromRoute]String userName)
		{
			var linkerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.LinkUser(linkerId, userName);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);
		}

		[HttpDelete("{userName}")]
		public async Task<IActionResult> UnLinkUser([FromRoute]String userName)
		{
			var linkerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.UnLinkUser(linkerId, userName);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);
		}

		[HttpGet("{userName}/Status")]
		public async Task<IActionResult> IsLinkingWith([FromRoute]String userName)
		{
			var linkerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.IsLinkingWith(linkerId, userName);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized(response);

			if (response.StatusCode == HttpStatusCode.BadRequest)
				return BadRequest(response);

			return Accepted(response);
		}

		[HttpGet("{userName}/MutualLinkers")]
		public async Task<IActionResult> GetMutualLinkers([FromRoute] String userName)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.GetMutualLinkersForUserAsync(userId, userName);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpGet("Linkers")]
		public async Task<IActionResult> GetLinkers()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.GetLinkersForUserAsync(userId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpGet("Linkings")]
		public async Task<IActionResult> GetLinkings()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _linkService.GetLinkingsForUserAsync(userId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

	}
}
