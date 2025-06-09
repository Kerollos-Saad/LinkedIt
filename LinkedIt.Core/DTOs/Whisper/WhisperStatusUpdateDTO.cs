using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Enums;

namespace LinkedIt.Core.DTOs.Whisper
{
	public class WhisperStatusUpdateDTO
	{
		[Required]
		public WhisperStatusEnum Status { get; set; }
	}
}
