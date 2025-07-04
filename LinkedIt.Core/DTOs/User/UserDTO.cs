﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.DTOs.User
{
	public class UserDTO
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public List<String> ErrorMessages { get; set; } = new List<String>();

		public string FullName => $"{FirstName}  {LastName}".Trim();
	}
}
