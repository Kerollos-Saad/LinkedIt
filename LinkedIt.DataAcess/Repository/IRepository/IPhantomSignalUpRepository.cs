using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IPhantomSignalUpRepository : IGenericRepository<PhantomSignalUp>
	{
		Task<bool> IsSignalUpByUser(String userId, Guid phantomSignalId);
		Task<bool> UpPhantomSignalAsync(String userId, Guid phantomSignalId);
		Task<bool> UpPhantomSignalUndoAsync(String userId, Guid phantomSignalId);
		Task<ICollection<String>> UserNamesUpPhantomSignalAsync(Guid phantomSignalId);
	}
}
