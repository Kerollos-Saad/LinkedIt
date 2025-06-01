using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Enums;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Response;

namespace LinkedIt.Services.ControllerServices.IControllerServices
{
	public interface IUserService
	{
		Task<APIResponse> GetUserProfileAsync(Expression<Func<ApplicationUser, bool>> filter,
			UserProfileQueryType queryType,
			string[]? includeProperties = null);

		Task<APIResponse> UpdateUserProfileAsync(String? userId, UpdateUserDTO userDto);

		Task<APIResponse> DeleteUserProfileAsync(String? userId);
	}
}
