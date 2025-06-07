using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.PhantomSignal;

namespace LinkedIt.Core.DTOs.PhantomResignal
{
	public class ResignalDetailsDTO
	{
		public ResignalDetailsDTO Resignal { get; set; }
		public ApplicationUserDTO UserResignal { get; set; }
		public PhantomSignalDTO PhantomSignal { get; set; }
		public ApplicationUserDTO UserSignal { get; set; }
	}
}
