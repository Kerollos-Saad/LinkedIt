using Azure;
using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Services.ControllerServices;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class WhisperTalkController : ControllerBase
	{
		private readonly IWhisperTalkService _whisperTalkService;
		public WhisperTalkController(IWhisperTalkService whisperTalkService)
		{
			this._whisperTalkService = whisperTalkService;
		}

		[HttpGet("{whisperId}")]
		public async Task<IActionResult> GetWhisperTalks([FromRoute] Guid whisperId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperTalkService.GetWhisperTalksForUserAsync(userId, whisperId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpPost("{whisperId}")]
		public async Task<IActionResult> AddWhisperTalk([FromRoute] Guid whisperId, AddWhisperTalkDTO addWhisperTalk)
		{
			var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperTalkService.AddWhisperTalkForUserAsync(senderId, whisperId, addWhisperTalk);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpDelete("{talkId}")]
		public async Task<IActionResult> RemoveWhisperTalk([FromRoute] int talkId)
		{
			var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperTalkService.RemoveWhisperTalkForUserAsync(senderId, talkId);

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
