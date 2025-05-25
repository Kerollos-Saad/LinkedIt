using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkedIt.DataAcess.Repository
{
	public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
	{

		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly IConfiguration _configuration; // To Get API Key [ Not Need It Until Now ]

		// Authentication
		private readonly IMapper _mapper;

		public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
			IConfiguration configuration, IMapper mapper) : base(db)
		{
			this._db = db;
			this._userManager = userManager;
			this._roleManager = roleManager;
			this._configuration = configuration;
			this._mapper = mapper;

		}

		public async Task<bool> IsUniqueUserName(string userName)
		{
			var user = await _db.ApplicationUsers
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.UserName == userName);
			return user == null;
		}

		public async Task<ApplicationUser> GetUserById(string userId)
		{
			var user = await _db.ApplicationUsers.FindAsync(userId);
			return user ?? throw new InvalidOperationException("User not found.");
		}

		public async Task<bool> UpdateAsync(String userId, UpdateUserDTO userDto)
		{
			var existingUser = await _db.ApplicationUsers.FindAsync(userId);

			if(existingUser == null) return false;

			// Updating Data ==> map from userDto → existingUser
			_mapper.Map(userDto, existingUser); // Only maps non-null fields from src to dest Cause it's configured at that way

			// Manually handle BirthDate only if it's not null
			if (userDto.BirthDate.HasValue)
				existingUser.BirthDate = userDto.BirthDate.Value;

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<UserWithRolesDTO?> GetUserWithRoles(String userName, String password)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null || !await _userManager.CheckPasswordAsync(user, password))
			{
				return null;
			}

			var userRoles = await _userManager.GetRolesAsync(user);

			return new UserWithRolesDTO()
			{
				User = user,
				Roles = userRoles
			};
		}

		public async Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO)
		{
			var user = new ApplicationUser
			{
				UserName = registerRequestDTO.UserName,
				FirstName = registerRequestDTO.FirstName,
				LastName = registerRequestDTO.LastName,
				Email = registerRequestDTO.Email,
				NormalizedEmail = registerRequestDTO.Email.ToUpper()
			};

			var userDTO = new UserDTO();

			try
			{
				var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
				if (result.Succeeded)
				{
					if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
					{
						foreach (var role in registerRequestDTO.Roles)
						{
							if (!await _roleManager.RoleExistsAsync(role))
							{
								await _roleManager.CreateAsync(new IdentityRole(role));
							}

							await _userManager.AddToRoleAsync(user, role);
						}
					}

					userDTO = _mapper.Map<UserDTO>(user);
				}
				else
				{
					userDTO.ErrorMessages = result.Errors.Select(e => e.Description).ToList();
				}
			}
			catch (Exception ex)
			{
				userDTO.ErrorMessages = new List<string> { "An unexpected error occurred while registering the user." };
			}

			return userDTO;
		}

		
	}
}
