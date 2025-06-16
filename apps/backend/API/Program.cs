using API.Entities.Other;
using API.Helpers;
using API.Infrastructure.Database;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DotNetEnv.Env.Load();

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var connectionString = builder.Configuration["ConnectionStrings:Default"];
            var jwtKey = builder.Configuration["Jwt:Key"];


            builder.Services.AddControllers();

            builder.Services.AddHttpClient();

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddSingleton<JwtHelper>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;
                return new JwtHelper(settings);
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<OnlineshopContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddLogging();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
            builder.Services.AddScoped<IMerchantService, MerchantService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
