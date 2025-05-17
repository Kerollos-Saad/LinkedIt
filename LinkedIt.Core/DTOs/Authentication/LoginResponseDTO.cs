using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.User;

namespace LinkedIt.Core.DTOs.Authentication
{
	public class LoginResponseDTO
	{
		public UserDTO User { get; set; }
		public String Token { get; set; }
	}
}
