using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.SignalComment
{
	public class SignalCommentDTO
	{
		public String UserName {  get; set; }
		public String Comment { get; set; }
		public DateTime SignalCommentDate { get; set; }
	}
}
