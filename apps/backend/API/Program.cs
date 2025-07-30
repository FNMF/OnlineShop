using API.Application.Common.EventBus;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Middlewares;
using API.Common.Models;
using API.Infrastructure.Database;
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

            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            var jwtKey = builder.Configuration["Jwt:SecretKey"];

            builder.Services.AddScoped<EventBus>();

            // 自动注册所有 Handler 实现类
            builder.Services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddSingleton<JwtHelper>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var settings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;
                return new JwtHelper(settings, configuration);
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

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ICurrentService, CurrentService>();

            builder.Services.Scan(scan => scan
                .FromApplicationDependencies(dep => dep.FullName.StartsWith("API"))
                .AddClasses(classes =>
                    classes.Where(type =>
                        type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")||type.Name.EndsWith("Factory")))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());


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
