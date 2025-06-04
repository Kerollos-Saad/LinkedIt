using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SignalReactionsController : ControllerBase
	{
		private readonly ISignalReactionsService _signalReactionsService;

		public SignalReactionsController(ISignalReactionsService signalReactionsService)
		{
			_signalReactionsService = signalReactionsService;
		}

		[HttpPut("UpSignal/{phantomSignalId}"), Authorize]
		public async Task<IActionResult> UpPhantomSignal([FromRoute] Guid phantomSignalId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalReactionsService.UpPhantomSignalForUserAsync(userId, phantomSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpPut("DownSignal/{phantomSignalId}"), Authorize]
		public async Task<IActionResult> DownPhantomSignal([FromRoute] Guid phantomSignalId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalReactionsService.DownPhantomSignalForUserAsync(userId, phantomSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpGet("{phantomSignalId}")]
		public async Task<IActionResult> PhantomSignalReactions([FromRoute] Guid phantomSignalId)
		{
			var response = await _signalReactionsService.PhantomSignalReactionsForUserAsync(phantomSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}
	}
}
