using LinkedIt.Services.ControllerServices;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using LinkedIt.Core.DTOs.SignalComment;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SignalCommentController : ControllerBase
	{
		private readonly ISignalCommentService _signalCommentService;
		public SignalCommentController(ISignalCommentService signalCommentService)
		{
			this._signalCommentService = signalCommentService;
		}

		[HttpPost("{phantomSignalId}"), Authorize]
		public async Task<IActionResult> AddCommentPhantomSignal([FromRoute] Guid phantomSignalId, PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalCommentService.AddCommentPhantomSignalForUserAsync(userId, phantomSignalId, phantomSignalCommentDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpPut("{commentId}"), Authorize]
		public async Task<IActionResult> UpdateCommentPhantomSignal([FromRoute] int commentId, PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalCommentService.UpdateCommentPhantomSignalForUserAsync(userId, commentId, phantomSignalCommentDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("V1/{commentId}")]
		public async Task<IActionResult> GetCommentPhantomSignal([FromRoute] int commentId)
		{
			var response = await _signalCommentService.GetCommentPhantomSignalForUserV1Async(commentId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("V2/{commentId}")]
		public async Task<IActionResult> GetCommentPhantomSignalV2([FromRoute] int commentId)
		{
			var response = await _signalCommentService.GetCommentPhantomSignalForUserV2Async(commentId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("V3/{commentId}")]
		public async Task<IActionResult> GetCommentPhantomSignalV3([FromRoute] int commentId)
		{
			var response = await _signalCommentService.GetCommentPhantomSignalForUserV3Async(commentId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}
	}
}
