using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.Whisper;
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

			builder.Entity<UserLink>()
				.ToTable("UserLink", "system");

			// Assign "system" schema to all entities in the Phantom_Signal, Whisper namespaces
			foreach (var entity in builder.Model.GetEntityTypes())
			{
				if (entity.ClrType?.Namespace == "LinkedIt.Core.Models.Phantom_Signal")
				{
					entity.SetSchema("system");
				}

				if (entity.ClrType?.Namespace == "LinkedIt.Core.Models.Whisper")
				{
					entity.SetSchema("system");
				}
			}

			builder.Entity<UserLink>()
				.HasOne(uf => uf.Linker)
				.WithMany(u => u.Linkings)
				.HasForeignKey(uf => uf.LinkerUserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<UserLink>()
				.HasOne(uf=>uf.Linked)
				.WithMany(u=>u.Linkers)
				.HasForeignKey(uf=>uf.LinkedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<UserLink> UserLinks { get; set; }

		public DbSet<PhantomSignal> PhantomSignals { get; set; }
		public DbSet<PhantomSignalUp> PhantomSignalsUps { get; set; }
		public DbSet<PhantomSignalDown> PhantomSignalsDowns { get; set; }
		public DbSet<PhantomSignalComment> PhantomSignalsComments { get; set; }
		public DbSet<PhantomResignal> PhantomResignals { get; set; }

		public DbSet<Whisper> Whispers { get; set; }
		public DbSet<WhisperTalk> WhispersTalks { get; set; }
	}
}
