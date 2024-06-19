using Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.Text;

namespace QuickStart.Extensions
{
    public static class UserManagerServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, UserRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;

                o.Lockout.AllowedForNewUsers = true;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                o.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securityKey"))
                };
            });
        }
    }
}
