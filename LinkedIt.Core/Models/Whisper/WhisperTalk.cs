using LinkedIt.Core.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Models.Whisper
{
	public class WhisperTalk
	{
		[Key]
		public int Id { get; set; }

		public DateTime TalkDate { get; set; } = DateTime.Now;

		public String TalkContent { get; set; }

		[ForeignKey("Whisper")]
		public Guid WhisperId { get; set; }
		public virtual Whisper Whisper { get; set; }

		[ForeignKey("Sender")]
		public String SenderId { get; set; }
		public virtual ApplicationUser Sender { get; set; }

		[NotMapped]
		public String ReceiverId => Whisper.SenderId == SenderId
			? Whisper.ReceiverId
			: Whisper.SenderId;
	}
}
