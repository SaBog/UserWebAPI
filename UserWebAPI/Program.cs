using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using UserWebAPI.Data;
using UserWebAPI.Repository;
using UserWebAPI.Repository.Interfaces;
using UserWebAPI.Services;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.Services.AddEndpointsApiExplorer();

			// add swagger
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });

				// adding jwt support
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Name = "Authorization",
					Description = "Please insert JWT with Bearer into field : \" bearer your_token \"",
					BearerFormat = "JWT"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				   {
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
                        Array.Empty<string>()
                    }
				});

				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});

			// connect to sql server
			var connectionString = builder.Configuration.GetConnectionString("MSSQL");
			builder.Services.AddDbContext<ApplicationDbContext>(e =>
			{
				e.UseSqlServer(connectionString);
			});

			// add services
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IRoleService, RoleService>();
			builder.Services.AddScoped<IJwtService, JwtService>();

			// add repositories
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IRoleRepository, RoleRepository>();

			// escape json cycles
			builder.Services.AddControllers().AddJsonOptions(x =>
							x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // add jwt authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidIssuer = AuthOptions.ISSUER,
						ValidAudience = AuthOptions.AUDIENCE,
						ValidateLifetime = true,

						ValidateIssuerSigningKey = true,
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
					};
				});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}

    }
}