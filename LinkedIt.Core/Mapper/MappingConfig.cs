using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Models.User;

namespace LinkedIt.Core.Mapper
{
	public class MappingConfig : AutoMapper.Profile
	{
		public MappingConfig()
		{
			CreateMap<ApplicationUser, UserDTO>().ReverseMap(); // BothWays
			CreateMap<ApplicationUser, ApplicationUserToAddUserDTO>().ReverseMap(); // BothWays
			CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap(); // BothWays

		}
	}
}
