using API.Api.SignalR;
using API.Application.Common.EventBus;
using API.Common.Helpers;
using API.Common.Models;
using API.Domain.Services.External;
using API.Infrastructure.Database;
using API.Infrastructure.test;
using API.Infrastructure.WechatPayV3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SKIT.FlurlHttpClient.Wechat.TenpayV3;
using System.Text;
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;

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

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddScoped<JwtHelper>();

            builder.Services.AddAuthorization();
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



            builder.Services.AddAuthentication()
                .AddJwtBearer("ExpiredAllowed", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false, // 允许过期
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                 });
            // 短时效注册令牌认证方案---注册时使用
            builder.Services.AddAuthentication()
                .AddJwtBearer("RegisterTempToken", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(2), // 允许2分钟的时间偏差
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var tokenType = context.Principal?
                                .FindFirst("Type")?
                                .Value;

                            if (tokenType != "RegisterTemp")
                            {
                                context.Fail("Not a RegisterTemp token.");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddDbContext<OnlineshopContext>(options =>
            {
                // AutoDetect云端莫名其妙报错，改用明确版本
                // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 27))
                );
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
                        type.Name.EndsWith("Service") || type.Name.EndsWith("Repository") || type.Name.EndsWith("Factory")))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

            // 自动注册所有 Handler 实现类
            builder.Services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            //WechatPayV3
            builder.Services.Configure<WeChatPayOptions>(
    builder.Configuration.GetSection("WeChatPay"));

            builder.Services.AddSingleton<WechatTenpayClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<WeChatPayOptions>>().Value;

                return new WechatTenpayClient(new WechatTenpayClientOptions()
                {
                    MerchantId = options.MchId,
                    MerchantV3Secret = options.MchV3Key, // APIv3 Key，必填
                    MerchantCertificateSerialNumber = options.MchCertificateSerialNumber,
                    MerchantCertificatePrivateKey = options.MchPrivateKey
                    // 其他可选配置按需设置
                });
            });

            //注册SignalR
            builder.Services.AddSignalR();

            var app = builder.Build();

            
            // Configure the HTTP request pipeline.

            //Hub路由注册
            app.MapHub<NotificationHub>("/hubs/notification");
            app.MapHub<ChatHub>("/hubs/chat");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = "/images"
            });


            app.MapControllers();

            app.Run();
        }
    }
}
