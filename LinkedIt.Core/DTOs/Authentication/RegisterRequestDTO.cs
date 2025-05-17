using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.Authentication
{
	public class RegisterRequestDTO
	{
		[Required]
		public String UserName { get; set; }
		[Required]
		public String FirstName { get; set; }
		[Required]
		public String LastName { get; set; }
		[Required]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		public String Password { get; set; }

		[Required]
		public List<String> Roles { get; set; } = new List<String>();
	}
}
