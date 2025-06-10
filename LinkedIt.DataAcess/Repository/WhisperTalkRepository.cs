using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Results;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class WhisperTalkRepository : GenericRepository<WhisperTalk>, IWhisperTalkRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public WhisperTalkRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<OperationResult<ICollection<WhisperTalkDetailsDTO>>> GetWhisperTalksAsync(Guid whisperId)
		{
			var talks = await _db.WhispersTalks
				.Where(t => t.WhisperId == whisperId)
				.ProjectTo<WhisperTalkDetailsDTO>(_mapper.ConfigurationProvider)
				.OrderBy(t=>t.TalkDate)
				.ToListAsync();

			return OperationResult<ICollection<WhisperTalkDetailsDTO>>.Success(talks);
		}

		public async Task<OperationResult<int>> AddWhisperTalkAsync(string senderId, Guid whisperId, AddWhisperTalkDTO addWhisperTalkDto)
		{
			var talk = new WhisperTalk
			{
				TalkDate = DateTime.Now,
				SenderId = senderId,
				WhisperId = whisperId,
				TalkContent = addWhisperTalkDto.TalkContent,
			};

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				await _db.WhispersTalks.AddAsync(talk);

				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return OperationResult<int>.Failure("Failed to save whisper");
				}

				await transaction.CommitAsync();
				return OperationResult<int>.Success(talk.Id);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return OperationResult<int>.Failure($"Exception occurred: {ex.Message}");
			}
		}

		public async Task<OperationResult<bool>> RemoveWhisperTalkAsync(int talkId)
		{
			var talk = await _db.WhispersTalks.FirstOrDefaultAsync(t => t.Id == talkId);

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				_db.WhispersTalks.Remove(talk!);

				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return OperationResult<bool>.Failure("Failed to Remove talk");
				}

				await transaction.CommitAsync();
				return OperationResult<bool>.Success(true);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return OperationResult<bool>.Failure($"Exception occurred: {ex.Message}");
			}
		}
	}
}
