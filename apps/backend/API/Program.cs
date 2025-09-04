using API.Api.SignalR;
using API.Application.Common.EventBus;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Middlewares;
using API.Common.Models;
using API.Domain.Events.MerchantCase;
using API.Infrastructure.Database;
using Microsoft.AspNetCore.HttpOverrides;
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

                    //SignalR握手带JWT
                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // 如果是 SignalR 请求，则尝试从查询字符串里取 token
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/hubs/notification"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<OnlineshopContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddLogging();
            //特殊名称服务注册
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IEventBus, EventBus>();
            //通用名称服务注册
            builder.Services.Scan(scan => scan
                .FromApplicationDependencies(dep => dep.FullName.StartsWith("API"))
                .AddClasses(classes =>
                    classes.Where(type =>
                        type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")||type.Name.EndsWith("Factory")))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

            //注册SignalR
            builder.Services.AddSignalR();

            //配置反向代理中间件
            //TODO,未完成

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            //Hub路由注册
            app.MapHub<NotificationHub>("/hubs/notification");
            app.MapHub<ChatHub>("/hubs/chat");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
