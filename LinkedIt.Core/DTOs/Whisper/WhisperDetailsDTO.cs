using LinkedIt.Core.Constants;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.Models.Whisper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.WhisperTalk;

namespace LinkedIt.Core.DTOs.Whisper
{
	public class WhisperDetailsDTO
	{
		public DateTime WhisperDate { get; set; }
		public String Status { get; set; }

		public ApplicationUserDTO Sender { get; set; }
		public ApplicationUserDTO Receiver { get; set; }
		public PhantomSignalDTO PhantomSignal { get; set; }

		public ICollection<WhisperTalkDetailsDTO> WhisperTalks { get; set; }
	}
}
