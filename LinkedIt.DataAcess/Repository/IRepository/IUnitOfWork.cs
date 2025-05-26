using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IUserRepository User { get; }

		Task<bool> SaveAsync();
	}
}
