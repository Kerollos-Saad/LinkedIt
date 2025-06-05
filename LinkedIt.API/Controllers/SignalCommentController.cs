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
	[Authorize]
	public class SignalCommentController : ControllerBase
	{
		private readonly ISignalCommentService _signalCommentService;
		public SignalCommentController(ISignalCommentService signalCommentService)
		{
			this._signalCommentService = signalCommentService;
		}

		[HttpPost("{phantomSignalId}")]
		public async Task<IActionResult> AddCommentPhantomSignal([FromRoute] Guid phantomSignalId, PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalCommentService.AddCommentPhantomSignalForUserAsync(userId, phantomSignalId, phantomSignalCommentDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}

		[HttpPut("{commentId}")]
		public async Task<IActionResult> UpdateCommentPhantomSignal([FromRoute] int commentId, PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _signalCommentService.UpdateCommentPhantomSignalForUserAsync(userId, commentId, phantomSignalCommentDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				_ => Ok(response)
			};
		}
	}
}
