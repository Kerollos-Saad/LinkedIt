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
		Task<bool> IsUniqueUserNameAsync(string userName);
		Task<ApplicationUser> GetUserByIdAsync(string userId);
		Task<bool> UpdateAsync(String userId, UpdateUserDTO userDto);

		// Authentication
		Task<UserWithRolesDTO> GetUserWithRolesAsync(String userName, String password);
		Task<UserDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO);
		
	}
}
