using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using DirectoryPermissionManagement.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;

namespace DirectoryPermissionManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("DirectoryPermissionDb"));

            // Register User Service
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserRepository>();

            // Register Drive Service
            builder.Services.AddScoped<DriveService>();
            builder.Services.AddScoped<DriveRepository>();

            // Register Folder Service
            builder.Services.AddScoped<FolderService>();
            builder.Services.AddScoped<FolderRepository>();

            // Register Item Service
            builder.Services.AddScoped<ItemService>();
            builder.Services.AddScoped<ItemRepository>();

            // Register Permission Service
            builder.Services.AddScoped<PermissionService>();
            builder.Services.AddScoped<PermissionRepository>();


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(option => {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter a valid token", Name = "Authorization", Type = SecuritySchemeType.Http, BearerFormat = "JWT", Scheme = "Bearer" });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                        new string[] { }
                    }
                });
            });

            //JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            //bind object model from configuration
            JWTConfig jwtConfig = builder.Configuration.GetSection("jwt").Get<JWTConfig>();

            //add it to services
            builder.Services.AddSingleton(jwtConfig);

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
