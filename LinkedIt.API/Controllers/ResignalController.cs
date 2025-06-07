using Azure;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using LinkedIt.Core.DTOs.PhantomResignal;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ResignalController : ControllerBase
	{
		private readonly IResignalService _resignalService;
		public ResignalController(IResignalService resignalService)
		{
			this._resignalService = resignalService;
		}

		[HttpGet("{reSignalId}")]
		public async Task<IActionResult> GetPhantomReSignal([FromRoute] int reSignalId)
		{
			var response = await _resignalService.GetPhantomReSignalForUserAsync(reSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("{reSignalId}/inDetails")]
		public async Task<IActionResult> GetPhantomReSignalInDetails([FromRoute] int reSignalId)
		{
			var response = await _resignalService.GetPhantomReSignalInDetailsForUserAsync(reSignalId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpPost("{phantomSignalId}")]
		public async Task<IActionResult> AddPhantomReSignal([FromRoute] Guid phantomSignalId, [FromBody] AddResignalDTO addPhantomReSignalDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _resignalService.AddPhantomReSignalForUserAsync(userId, phantomSignalId, addPhantomReSignalDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				HttpStatusCode.Created => Created(
					uri: $"/api/resignal/{response.Result}",
					value: response),
				_ => Ok(response)
			};
		}

		[HttpPut("{reSignalId}")]
		public async Task<IActionResult> UpdatePhantomReSignal([FromRoute] int reSignalId, [FromBody] AddResignalDTO updatePhantomResignalDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _resignalService.UpdatePhantomReSignalForUserAsync(userId, reSignalId, updatePhantomResignalDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		//[HttpDelete]
		//public async Task<IActionResult> DeletePhantomReSignal([FromBody] Guid phantomResignalId)
		//{
		//	var userId = User.FindFirst(ClaimTypes.NameIdentifier);

		//	var response = await _resignalService.DeletePhantomReSignalForUserAsync(userId, phantomResignalId);

		//	return response.StatusCode switch
		//	{
		//		HttpStatusCode.Unauthorized => Unauthorized(response),
		//		HttpStatusCode.BadRequest => BadRequest(response),
		//		HttpStatusCode.NotFound => NotFound(response),
		//		_ => Ok(response)
		//	};
		//}
	}
}
