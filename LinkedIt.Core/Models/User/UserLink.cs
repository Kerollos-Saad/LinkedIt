using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Models.User
{
	public class UserLink
	{
		public Guid Id { get; set; }
		public DateTime FollowDate { get; set; }

		[ForeignKey("Linker")]
		public required string LinkerUserId { get; set; }
		public required ApplicationUser Linker { get; set; }


		[ForeignKey("Linked")]
		public required string LinkedUserId { get; set; }
		public required ApplicationUser Linked { get; set; }
	}
}
