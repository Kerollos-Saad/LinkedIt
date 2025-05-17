using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LinkedIt.DataAcess.Repository
{
	internal class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
	{

		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly IConfiguration _configuration; // To Get API Key
		private string securityKey;

		public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) : base(db)
		{
			this._db = db;
			this._userManager = userManager;
			this._roleManager = roleManager;
			this._configuration = configuration;

			//Need To install Package `Microsoft.Extensions.Configuration.Binder` and the method `GetValue` will be available
			securityKey = _configuration.GetValue<string>("ApiSettings:Secret") ??
			              throw new InvalidOperationException("ApiSettings:Secret is not configured.");

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
	}
}
