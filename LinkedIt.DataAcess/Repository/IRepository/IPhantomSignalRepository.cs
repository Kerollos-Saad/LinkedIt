using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Models.Phantom_Signal;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IPhantomSignalRepository : IGenericRepository<PhantomSignal>
	{
		Task<bool> IsSignalExist(Guid phantomSignalId);
		Task<bool> IsSignalHisPropertyAsync(String userId, Guid phantomSignalId);
		Task<bool> UpdatePhantomSignalAsync(Guid phantomSignalId, AddPhantomSignalDTO phantomSignal);
	}
}
