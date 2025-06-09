using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.Constants;
using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.Whisper;
using LinkedIt.Core.Results;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository
{
	public class WhisperRepository : GenericRepository<Whisper>, IWhisperRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public WhisperRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<bool> IsWhisperHisPropertyAsync(string userId, Guid whisperId)
		{
			var found = await _db.Whispers
				.AnyAsync(w => w.Id == whisperId
				               && (w.SenderId == userId || w.ReceiverId == userId));
			return found;
		}

		public async Task<WhisperDetailsDTO> GetWhisperDetailsAsync(Guid whisperId)
		{
			var whisperDetailsDto = await _db.Whispers
				.Where(w => w.Id == whisperId)
				.ProjectTo<WhisperDetailsDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			return whisperDetailsDto!;
		}

		public async Task<OperationResult<Guid>> AddWhisperWithExistPhantomSignalAsync(string senderId, AddWhisperWithExistPhantomSignalDTO addWhisperDto)
		{
			var whisper = new Whisper
			{
				WhisperDate = DateTime.Now,
				Status = WhisperStatus.WhisperStatusPending,
				SenderId = senderId,
				ReceiverId = addWhisperDto.ReceiverId,
				PhantomSignalId = addWhisperDto.phantomSignalId
			};
			
			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				await _db.Whispers.AddAsync(whisper);

				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return OperationResult<Guid>.Failure("Failed to save whisper");
				}

				await transaction.CommitAsync();
				return OperationResult<Guid>.Success(whisper.Id);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return OperationResult<Guid>.Failure($"Exception occurred: {ex.Message}");
			}
		}

		public async Task<OperationResult<Guid>> AddWhisperWithNewPhantomSignalAsync(string senderId, AddWhisperWithNewPhantomSignalDTO addWhisperDto)
		{
			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				var phantomSignal = new PhantomSignal
				{
					PhantomFlag = true,
					SignalContent = addWhisperDto.PhantomSignal.SignalContent,
					SignalDate = DateTime.UtcNow,
					ApplicationUserId = senderId,
				};

				await _db.PhantomSignals.AddAsync(phantomSignal);
				await _db.SaveChangesAsync();

				var whisper = new Whisper
				{
					WhisperDate = DateTime.UtcNow,
					Status = WhisperStatus.WhisperStatusPending,
					SenderId = senderId,
					ReceiverId = addWhisperDto.ReceiverId,
					PhantomSignalId = phantomSignal.Id
				};

				await _db.Whispers.AddAsync(whisper);
				await _db.SaveChangesAsync();

				await transaction.CommitAsync();
				return OperationResult<Guid>.Success(whisper.Id);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return OperationResult<Guid>.Failure($"Exception occurred: {ex.Message}");
			}
		}
	}
}
