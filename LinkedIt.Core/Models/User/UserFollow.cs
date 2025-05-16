using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Models.User
{
	public class UserFollow
	{
		public Guid Id { get; set; }
		public DateTime FollowDate { get; set; }

		[ForeignKey("Follower")]
		public string FollowerUserId { get; set; }
		public ApplicationUser Follower { get; set; }


		[ForeignKey("Followed")]
		public string FollowedUserId { get; set; }
		public ApplicationUser Followed { get; set; }
	}
}
