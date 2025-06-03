using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.PhantomSignal
{
	public class PhantomSignalDTO
	{
		public Guid Id { get; set; }

		public bool PhantomFlag { get; set; }

		public String SignalContent { get; set; }
		public DateTime SignalDate { get; set; }

		public int UpCount { get; set; }
		public int DownCount { get; set; }
		public int CommentCount { get; set; }
		public int ResignalCount { get; set; }

		public string ApplicationUserId { get; set; }
	}
}
