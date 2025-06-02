using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;

namespace LinkedIt.Core.Models.Phantom_Signal
{
	public class PhantomSignal
	{
		[Key]
		public Guid Id { get; set; }

		public bool PhantomFlag { get; set; } = false;

		public String SignalContent { get; set; }
		public DateTime SignalDate { get; set; } = DateTime.Now;

		public int UpCount { get; set; }
		public int DownCount { get; set; }
		public int CommentCount { get; set; }
		public int ResignalCount { get; set; }

		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }


		public virtual ICollection<PhantomSignalUp> PhantomSignalUps { get; set; } = new HashSet<PhantomSignalUp>();
		public virtual ICollection<PhantomSignalDown> PhantomSignalDowns { get; set; } = new HashSet<PhantomSignalDown>();
		public virtual ICollection<PhantomSignalComment> PhantomSignalComments { get; set; } = new HashSet<PhantomSignalComment>();
		public virtual ICollection<PhantomResignal> PhantomResignals { get; set; } = new HashSet<PhantomResignal>();
	}
}
