using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomResignalRepository : GenericRepository<PhantomResignal>, IPhantomResignalRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public PhantomResignalRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<ResignalDetailsDTO> GetPhantomReSignalInDetailsAsync(int reSignalId)
		{
			var resignalDetailsDto = await _db.PhantomResignals
				.Where(r => r.Id == reSignalId)
				.ProjectTo<ResignalDetailsDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			return resignalDetailsDto;
		}

		public async Task<int> AddPhantomReSignalAsync(string userId, Guid phantomSignalId, AddResignalDTO addResignalDto)
		{
			var signal = await _db.PhantomSignals
				.FirstOrDefaultAsync(s => s.Id == phantomSignalId);

			var reSignal = new PhantomResignal
			{
				ReSignalContent = addResignalDto.ReSignalContent,
				ResignalDate = DateTime.Now,
				ApplicationUserId = userId,
				PhantomSignalId = phantomSignalId
			};

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				await _db.PhantomResignals.AddAsync(reSignal);
				signal.ResignalCount++;

				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return 0;
				}

				await transaction.CommitAsync();
				return reSignal.Id;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}

		public async Task<bool> IsResignalHisPropertyAsync(string userId, int reSignalId)
		{
			var reSignalExist = await _db.PhantomResignals.AnyAsync(r => r.Id == reSignalId && r.ApplicationUserId == userId);
			return reSignalExist;
		}

		public async Task<bool> UpdatePhantomReSignalAsync(int reSignalId, AddResignalDTO updateResignalDto)
		{
			var existResignal = await _db.PhantomResignals.FirstOrDefaultAsync(r => r.Id == reSignalId);

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				existResignal.ResignalDate = DateTime.Now;
				existResignal.ReSignalContent = updateResignalDto.ReSignalContent;

				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return false;
				}

				await transaction.CommitAsync();
				return true;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}

		public async Task<bool> DeletePhantomReSignalAsync(int reSignalId)
		{
			var existResignal = await _db.PhantomResignals.FirstOrDefaultAsync(r => r.Id == reSignalId);

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				_db.Remove(existResignal!);
				var success = await _db.SaveChangesAsync();
				if (success < 1)
				{
					await transaction.RollbackAsync();
					return false;
				}

				await transaction.CommitAsync();
				return true;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}
	}
}
