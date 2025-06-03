using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomSignalUpRepository : GenericRepository<PhantomSignalUp>, IPhantomSignalUpRepository
	{
		private readonly ApplicationDbContext _db;
		public PhantomSignalUpRepository(ApplicationDbContext db) : base(db)
		{
			this._db = db;
		}

		public async Task<bool> IsSignalUpByUser(string userId, Guid phantomSignalId)
		{
			var up = await _db.PhantomSignalsUps.FirstOrDefaultAsync(u=>
				u.ApplicationUserId == userId && 
				u.PhantomSignalId == phantomSignalId);

			return up != null;
		}


		public async Task<bool> UpPhantomSignalAsync(string userId, Guid phantomSignalId)
		{
			var upSignal = new PhantomSignalUp
			{
				SignalUpDate = DateTime.Now,
				ApplicationUserId = userId,
				PhantomSignalId = phantomSignalId,
			};

			var existPhantomSignal = await _db.PhantomSignals.FindAsync(phantomSignalId);
			existPhantomSignal.UpCount++;

			await _db.PhantomSignalsUps.AddAsync(upSignal);

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<bool> UpPhantomSignalUndoAsync(string userId, Guid phantomSignalId)
		{
			var up = await _db.PhantomSignalsUps.FirstOrDefaultAsync(u =>
				u.ApplicationUserId == userId &&
				u.PhantomSignalId == phantomSignalId);

			_db.PhantomSignalsUps.Remove(up);

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}
	}
}
