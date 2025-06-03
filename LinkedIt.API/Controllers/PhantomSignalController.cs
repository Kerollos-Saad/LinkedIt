using System.Net;
using System.Security.Claims;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PhantomSignalController : ControllerBase
	{
		private readonly IPhantomSignalService _phantomSignalService;

		public PhantomSignalController(IPhantomSignalService phantomSignalService)
		{
			this._phantomSignalService = phantomSignalService;
		}

		[HttpGet("{PhantomSignalId}")]
		public async Task<IActionResult> GetPhantomSignal([FromRoute] Guid PhantomSignalId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _phantomSignalService.GetPhantomSignalAsync(userId, PhantomSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpPost]
		public async Task<IActionResult> AddPhantomSignal([FromBody] AddPhantomSignalDTO phantomSignalDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _phantomSignalService.AddPhantomSignalAsync(userId, phantomSignalDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}


	}
}
