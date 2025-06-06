using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.AppUsers
{
	public class ApplicationUserDTO
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Bio { get; set; }
		public string location { get; set; }
		public string Gender { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime UserCreated { get; set; }

		public string FullName => $"{FirstName} {LastName}".Trim();
	}
}
