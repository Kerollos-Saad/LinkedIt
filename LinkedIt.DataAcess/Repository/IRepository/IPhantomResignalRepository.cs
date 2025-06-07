using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.Models.Phantom_Signal;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IPhantomResignalRepository : IGenericRepository<PhantomResignal>
	{
		Task<ResignalDetailsDTO> GetPhantomReSignalInDetailsAsync(int reSignalId);
	}
}
