using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.PhantomSignal;

namespace LinkedIt.Core.DTOs.SignalComment
{
	public class SignalCommentDetailsDTO
	{
		public int? Id { get; set; }
		public String? Comment { get; set; }
		public DateTime? SignalCommentDate { get; set; }

		public ApplicationUserDTO ApplicationUser { get; set; }

		public PhantomSignalDTO PhantomSignal { get; set; }
	}
}
