﻿using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Services.ControllerServices;
using LinkedIt.Services.ControllerServices.IControllerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using LinkedIt.Core.DTOs.Whisper;

namespace LinkedIt.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class WhisperController : ControllerBase
	{
		private readonly IWhisperService _whisperService;
		public WhisperController(IWhisperService whisperService)
		{
			this._whisperService = whisperService;
		}

		[HttpGet("{whisperId}")]
		public async Task<IActionResult> GetWhisper([FromRoute] Guid whisperId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.GetWhisperForUserAsync(userId, whisperId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("{whisperId}/Details")]
		public async Task<IActionResult> GetWhisperDetails([FromRoute] Guid whisperId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.GetWhisperDetailsForUserAsync(userId, whisperId);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpGet("status-options")]
		public IActionResult GetWhisperStatusOptions()
		{
			var response = _whisperService.GetWhisperStatusOptions();
			return Ok(response);
		}

		[HttpPost("ExistSignal")]
		public async Task<IActionResult> AddWhisperWithExistPhantomSignal([FromBody] AddWhisperWithExistPhantomSignalDTO addWhisperWithExistPhantomSignalDto)
		{
			var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.AddWhisperWithExistPhantomSignalForUserAsync(senderId, addWhisperWithExistPhantomSignalDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpPost("NewSignal")]
		public async Task<IActionResult> AddWhisperWithNewPhantomSignal([FromBody] AddWhisperWithNewPhantomSignalDTO addWhisperWithNewPhantomSignalDto)
		{
			var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.AddWhisperWithNewPhantomSignalForUserAsync(senderId, addWhisperWithNewPhantomSignalDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
			return Ok();
		}

		[HttpPut("{whisperId}/Status")]
		public async Task<IActionResult> UpdateWhisperStatus([FromRoute] Guid whisperId,[FromBody] WhisperStatusUpdateDTO whisperStatusUpdateDto)
		{
			var receiverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.UpdateWhisperStatusForUserAsync(receiverId, whisperId, whisperStatusUpdateDto);

			return response.StatusCode switch
			{
				HttpStatusCode.Unauthorized => Unauthorized(response),
				HttpStatusCode.BadRequest => BadRequest(response),
				HttpStatusCode.NotFound => NotFound(response),
				_ => Ok(response)
			};
		}

		[HttpDelete("{whisperId}")]
		public async Task<IActionResult> RemoveWhisper([FromRoute] Guid whisperId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var response = await _whisperService.RemoveWhisperForUserAsync(userId, whisperId);

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
