using System.Text;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Mapper;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository;
using LinkedIt.DataAcess.Repository.IRepository;
using LinkedIt.Services.ControllerServices;
using LinkedIt.Services.ControllerServices.IControllerServices;
using LinkedIt.Services.JWTService;
using LinkedIt.Services.JWTService.IJWTService;
using LinkedIt.Services.ServiceRegistration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

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

			//// No need for Register Generic Repository We Will Deal With Unit Of Work Until now 
			// Register Unit Of Work
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Register Configure Identity 
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.AddScoped<UserManager<ApplicationUser>>();
			builder.Services.AddScoped<SignInManager<ApplicationUser>>();
			builder.Services.AddScoped<RoleManager<IdentityRole>>();

			// Add Services
			builder.Services.AddApplicationServices();

			// Add OpenAPI with Bearer Authentication Support
			#region JWT


			// Add Jwt Token in Scalar
			builder.Services.AddOpenApi("v1", options =>
			{
				options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
			});

			// Add Jwt Token in Swagger
			builder.Services.AddSwaggerGen(option =>
			{
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			});

			// Configure JWT Authentication instead of cookies
			var key = Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:Key"]);
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.FromMinutes(5)
				};
			});

			#endregion


			// Add services to the container.
			builder.Services.AddControllers();

            // Register Swagger services
            builder.Services.AddEndpointsApiExplorer();   // Required
            //builder.Services.AddSwaggerGen();           // Required Use Custom One With Bearer Jwt

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();

				// Enable Swagger
				app.UseSwagger();
				app.UseSwaggerUI();

				// Enable Scalar
				app.MapScalarApiReference(options =>
				{
					options.WithTheme(ScalarTheme.Mars);
				});
			}

			// app.UseAuthentication(); By default Work By "Cookie" ==> To Work With JWT ==> AddAuthentication(options)
			app.UseAuthentication();
			app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
