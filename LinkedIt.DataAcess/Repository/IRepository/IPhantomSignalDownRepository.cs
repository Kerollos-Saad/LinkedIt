using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IPhantomSignalDownRepository : IGenericRepository<PhantomSignalDown>
	{
		Task<bool> IsSignalDownByUser(String userId, Guid phantomSignalId);
		Task<bool> DownPhantomSignalAsync(String userId, Guid phantomSignalId);
		Task<bool> DownPhantomSignalUndoAsync(String userId, Guid phantomSignalId);
	}
}
