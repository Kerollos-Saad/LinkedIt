using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedIt.Core.Models.User
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(30)]
		public string? FirstName { get; set; }
		[MaxLength(30)]
		public string? LastName { get; set; }

		public string? Bio { get; set; }
		public string? Location { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime UserCreated { get; set; }
		public string? Gender { get; set; }



		public ICollection<UserLink>? Linkers { get; set; }
		public ICollection<UserLink>? Linkings { get; set; }

		[NotMapped]
		public string FullName => $"{FirstName} {LastName}".Trim();
	}
}
