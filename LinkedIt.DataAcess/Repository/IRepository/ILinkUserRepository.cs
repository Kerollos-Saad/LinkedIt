using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface ILinkUserRepository
	{
		Task<bool?> IsAlreadyLinking(String linkerId, String linkedId);

		Task<bool> LinkUser(String linkerId, String linkedId);
	}
}
