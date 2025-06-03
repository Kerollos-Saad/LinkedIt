using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomSignalRepository : GenericRepository<PhantomSignal>, IPhantomSignalRepository
	{
		private readonly ApplicationDbContext _db;
		public PhantomSignalRepository(ApplicationDbContext db) : base(db)
		{
			this._db = db;
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
	}
}
