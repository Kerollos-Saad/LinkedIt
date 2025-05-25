using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.User
{
	public class UpdateUserDTO
	{
		public string? UserName { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Bio { get; set; }
		public string? Location { get; set; }
		public string? Gender { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime? BirthDate { get; set; }
	}
}
