using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Constants;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.User;

namespace LinkedIt.Core.Models.Whisper
{
	public class Whisper
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime WhisperDate { get; set; } = DateTime.Now; 
		public String Status { get; set; } = WhisperStatus.WhisperStatusPending;

		[ForeignKey("Sender")]
		public String SenderId { get; set; }
		public virtual ApplicationUser Sender { get; set; }

		[ForeignKey("Receiver")]
		public String ReceiverId { get; set; }
		public virtual ApplicationUser Receiver { get; set; }

		[ForeignKey("PhantomSignal")]
		public Guid PhantomSignalId { get; set; }
		public virtual PhantomSignal PhantomSignal { get; set; }

		public virtual ICollection<WhisperTalk> WhisperTalks { get; set; } = new HashSet<WhisperTalk>();

	}
}
