using LinkedIt.Core.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Models.Phantom_Signal
{
	public class PhantomResignal
	{
		[Key]
		public int Id { get; set; }
		public DateTime ResignalDate { get; set; } = DateTime.Now;
		public String? ReSignalContent { get; set; }

		[ForeignKey("PhantomSignal")]
		public Guid PhantomSignalId { get; set; }
		public virtual PhantomSignal PhantomSignal { get; set; }

		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
