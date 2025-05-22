using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;

namespace LinkedIt.Core.DTOs.AppUsers
{
	public class UserWithRolesDTO
	{
		public ApplicationUser User { get; set; } = null;
		public IList<String> Roles { get; set; } = new List<String>();
	}
}
