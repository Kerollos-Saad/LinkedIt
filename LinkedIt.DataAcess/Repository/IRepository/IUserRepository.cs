using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	internal interface IUserRepository : IGenericRepository<ApplicationUser>
	{
		Task<bool> IsUniqueUserName(string userName);
		Task<ApplicationUser> GetUserById(string userId);
		Task<bool> UpdateAsync(ApplicationUser user);
	}
}
