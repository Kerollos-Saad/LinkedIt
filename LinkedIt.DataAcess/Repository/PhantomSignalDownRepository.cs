using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomSignalDownRepository : GenericRepository<PhantomSignalDown>, IPhantomSignalDownRepository
	{
		private readonly ApplicationDbContext _db;
		public PhantomSignalDownRepository(ApplicationDbContext db):base(db)
		{
			this._db = db;
		}

		public async Task<bool> IsSignalDownByUser(string userId, Guid phantomSignalId)
		{
			var down = await _db.PhantomSignalsDowns.FirstOrDefaultAsync(d => 
				d.ApplicationUserId == userId && 
			    d.PhantomSignalId == phantomSignalId);

			return down != null;
		}

		public async Task<bool> DownPhantomSignalAsync(string userId, Guid phantomSignalId)
		{
			var downSignal = new PhantomSignalDown
			{
				SignalDownDate = DateTime.Now,
				ApplicationUserId = userId,
				PhantomSignalId = phantomSignalId
			};

			var existPhantomSignal = await _db.PhantomSignals.FindAsync(phantomSignalId);
			existPhantomSignal.DownCount++;

			await _db.PhantomSignalsDowns.AddAsync(downSignal);

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<bool> DownPhantomSignalUndoAsync(string userId, Guid phantomSignalId)
		{
			var down = await _db.PhantomSignalsDowns.FirstOrDefaultAsync(d => 
				d.ApplicationUserId == userId &&
				d.PhantomSignalId == phantomSignalId);

			var existPhantomSignal = await _db.PhantomSignals.FindAsync(phantomSignalId);
			existPhantomSignal.DownCount--;

			_db.PhantomSignalsDowns.Remove(down);

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<ICollection<string?>> UserNamesDownPhantomSignalAsync(Guid phantomSignalId)
		{
			var userNames = await _db.PhantomSignalsDowns
				.Where(d=>d.PhantomSignalId == phantomSignalId)
				.Select(u => u.ApplicationUser.UserName)
				.ToListAsync();
			
			return userNames;
		}
	}
}
