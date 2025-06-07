using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomSignalRepository : GenericRepository<PhantomSignal>, IPhantomSignalRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public PhantomSignalRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<bool> IsSignalExist(Guid phantomSignalId)
		{
			var result = await _db.PhantomSignals.FindAsync(phantomSignalId);
			return result != null;
		}

		public async Task<bool> IsSignalHisPropertyAsync(string userId, Guid phantomSignalId)
		{
			var phantomSignal = await _db.PhantomSignals.FindAsync(phantomSignalId);
			return phantomSignal != null && phantomSignal.ApplicationUserId == userId;
		}

		public async Task<bool> UpdatePhantomSignalAsync(Guid phantomSignalId, AddPhantomSignalDTO phantomSignal)
		{
			var existPhantomSignal = await _db.PhantomSignals.FindAsync(phantomSignalId);

			existPhantomSignal.SignalDate = DateTime.Now;
			existPhantomSignal.SignalContent = phantomSignal.SignalContent;

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<PhantomSignalDetailsDTO> GetPhantomSignalDetailsAsync(string userId, Guid phantomSignalId)
		{
			// Without .Include and .ThenInclude EF Will Automatically join tables to best practice join [ save resources ]
			var signalDetailsDto = await _db.PhantomSignals
				.Where(s => s.Id == phantomSignalId)
				.ProjectTo<PhantomSignalDetailsDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			return signalDetailsDto;
		}
	}
}
