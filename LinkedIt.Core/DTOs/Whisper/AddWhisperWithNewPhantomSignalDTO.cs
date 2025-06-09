using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.PhantomSignal;

namespace LinkedIt.Core.DTOs.Whisper
{
	public class AddWhisperWithNewPhantomSignalDTO
	{
		public String ReceiverId { get; set; }

		public AddPhantomSignalDTO PhantomSignal { get; set; }
	}
}
