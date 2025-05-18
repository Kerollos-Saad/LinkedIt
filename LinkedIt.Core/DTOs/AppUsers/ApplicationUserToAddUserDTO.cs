using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.AppUsers
{
	public class ApplicationUserToAddUserDTO
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Bio { get; set; }
		public string Location { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime UserCreated { get; set; }
		public string Gender { get; set; }

		public string email { get; set; }
		public string Password { get; set; }
		public string phoneNumber { get; set; }
	}
}
