using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.Linker;
using LinkedIt.Core.Models.User;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface ILinkUserRepository : IGenericRepository<UserLink>
	{
		Task<bool> IsAlreadyLinking(String linkerId, String linkedId);

		Task<bool> LinkUser(String linkerId, String linkedId);
		Task<bool> UnLinkUser(String linkerId, String linkedId);

		Task<IEnumerable<ApplicationUser>> GetMutualLinkersAsync(String userId, String targetUserId);

		Task<List<LinkerDTO>> GetLinkersDtoAsync(String userId);
		Task<List<LinkerDTO>> GetLinkingsDtoAsync(String userId);

		Task<int> GetLinkersCountAsync(String userId);
		Task<int> GetLinkingsCountAsync(String userId);

		Task<(int linkersCount, int linkingsCount)> GetLinkers_LinkingsCountAsync(String userId);
	}
}
