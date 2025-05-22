using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.DTOs.User;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IUserRepository : IGenericRepository<ApplicationUser>
	{
		Task<bool> IsUniqueUserName(string userName);
		Task<ApplicationUser> GetUserById(string userId);
		Task<bool> UpdateAsync(ApplicationUser user);

		// Authentication
		Task<UserWithRolesDTO> GetUserWithRoles(String userName, String password);
		Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO);
		
	}
}
