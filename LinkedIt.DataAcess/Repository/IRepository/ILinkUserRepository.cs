using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface ILinkUserRepository
	{
		Task<bool?> IsAlreadyLinking(String linkerId, String linkedId);

		Task<bool> LinkUser(String linkerId, String linkedId);
		Task<bool> UnLinkUser(String linkerId, String linkedId);

		Task<IEnumerable<ApplicationUser>> GetMutualLinkersAsync(String userId, String targetUserId);

		Task<IEnumerable<ApplicationUser>> GetLinkersAsync(String userId);
	}
}
