﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.Authentication
{
	public class LoginRequestDTO
	{
		[Required]
		public String UserName { get; set; }
		[Required]
		public String Password { get; set; }
	}
}
