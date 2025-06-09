using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.Whisper
{
	public class AddWhisperWithExistPhantomSignalDTO
	{
		public Guid phantomSignalId { get; set; }
		public String ReceiverId { get; set; }
	}
}
