using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.WhisperTalk
{
	public class WhisperTalkDetailsDTO
	{
		public DateTime TalkDate { get; set; } 
		public String TalkContent { get; set; }
		public String SenderUserName { get; set; }
		public Guid WhisperId { get; set; }
	}
}
