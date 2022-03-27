using DeviceManager.Busniess.Services;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
using DeviceManager.Busniess.Table_filters.Factories;
using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Filters.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeviceManager.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddTableFilters(this IServiceCollection services)
        {
            services.AddScoped<IDeviceTableFilterFactory, DeviceTableFilterFactory>();
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceMapper, DeviceMapper>();
        }
        public static void AddEfCore(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<DeviceManagerDBContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<DeviceManagerDBContext>()
               .AddDefaultTokenProviders();

        }
        public static void AddAuthenticationService(this IServiceCollection services, string tokenKey)
        {
            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IAuthenticationService>(new JWTAuthenticationService(tokenKey));

        }
        public static void AddAllowAnyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                });
            });
        }
    }
}
