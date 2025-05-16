using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinkedIt.Core.Models.User;

namespace LinkedIt.DataAcess.Context
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
			:base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>()
				.ToTable("Users", "Security");
			builder.Entity<UserFollow>()
				.ToTable("UserFollow", "security");

			builder.Entity<IdentityRole>()
				.ToTable("Roles", "security");
			builder.Entity<IdentityUserRole<String>>()
				.ToTable("UserRoles", "security");
			builder.Entity<IdentityUserClaim<String>>()
				.ToTable("UserClaims", "security");
			builder.Entity<IdentityUserLogin<String>>()
				.ToTable("UserLogins", "security");
			builder.Entity<IdentityRoleClaim<String>>()
				.ToTable("RoleClaims", "security");
			builder.Entity<IdentityUserToken<String>>()
				.ToTable("UserTokens", "security");


			builder.Entity<UserFollow>()
				.HasOne(uf => uf.Follower)
				.WithMany(u => u.Followings)
				.HasForeignKey(uf => uf.FollowerUserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<UserFollow>()
				.HasOne(uf=>uf.Followed)
				.WithMany(u=>u.Followers)
				.HasForeignKey(uf=>uf.FollowedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<UserFollow> UserFollows { get; set; }
	}
}
