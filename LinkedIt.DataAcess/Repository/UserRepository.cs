using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.DTOs.Authentication;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkedIt.DataAcess.Repository
{
	internal class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
	{

		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly IConfiguration _configuration; // To Get API Key
		private string securityKey;
		private int tokenDuration;

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

			//Need To install Package `Microsoft.Extensions.Configuration.Binder` and the method `GetValue` will be available
			securityKey = _configuration.GetValue<string>("JWTSettings:Key") ??
			              throw new InvalidOperationException("JWTSettings:Key is not configured.");
			tokenDuration = _configuration.GetValue<int?>("JWTSettings:DurationInMinutes") ??
			                throw new InvalidOperationException("JWTSettings:Key is not configured.");
		}

		public async Task<bool> IsUniqueUserName(string userName)
		{
			var user = await _db.ApplicationUsers.FindAsync(userName);
			return user == null;
		}

		public async Task<ApplicationUser> GetUserById(string userId)
		{
			var user = await _db.ApplicationUsers.FindAsync(userId);
			return user ?? throw new InvalidOperationException("User not found.");
		}

		public async Task<bool> UpdateAsync(ApplicationUser user)
		{
			var existingUser = await _db.ApplicationUsers.FindAsync(user.Id);

			if(existingUser == null) return false;

			// Updating Data

			var result = await _db.SaveChangesAsync();
			return result > 0;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var user = await _userManager.FindByNameAsync(loginRequestDTO.UserName);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
			{
				return new LoginResponseDTO()
				{
					Token = "",
					User = null
				};
			}

			var userRoles = await _userManager.GetRolesAsync(user);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(tokenDuration),
				signingCredentials: credentials
			);

			return new LoginResponseDTO()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				User = _mapper.Map<UserDTO>(user)
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
