using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using LinkedIt.Services.ControllerServices.IControllerServices;

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

		[HttpPut("UpSignal/{phantomSignalId}")]
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
	}
}
