using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.DTOs.SignalReaction;

namespace LinkedIt.Core.DTOs.PhantomSignal
{
	public class PhantomSignalDetailsDTO
	{
		public PhantomSignalDTO PhantomSignal { get; set; }
		public ApplicationUserDTO SignalUser { get; set; }

		public ICollection<SignalUpDTO> SignalUps { get; set; }
		public ICollection<SignalDownDTO> SignalDowns { get; set; }
		public ICollection<SignalCommentDTO> SignalComments { get; set; }
		public ICollection<ResignalDTO> Resignals { get; set; }
	}
}
