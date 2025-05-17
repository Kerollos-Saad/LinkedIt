using LinkedIt.Core.Models.User;
using LinkedIt.Core.Mapper;
using LinkedIt.DataAcess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add Connection String.
            var connectionString = builder.Configuration.GetConnectionString("Development") ?? throw new InvalidOperationException("Connection string 'Development' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
	            options.UseSqlServer(connectionString));

            // Register Auto Mapper
            builder.Services.AddAutoMapper(typeof(MappingConfig));

			// // Register Generic Repository 

			// Register Unit Of Work

			// Register Configure Identity 
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.AddScoped<UserManager<ApplicationUser>>();
			builder.Services.AddScoped<SignInManager<ApplicationUser>>();
			builder.Services.AddScoped<RoleManager<IdentityRole>>();


            // Add Services






			// Add services to the container.
			builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
