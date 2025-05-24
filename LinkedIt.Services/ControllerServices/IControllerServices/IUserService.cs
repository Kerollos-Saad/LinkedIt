using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

	}
}
