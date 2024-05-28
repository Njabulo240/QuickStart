using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Service.Contracts;

namespace QuickStart.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                 .WithOrigins("http://localhost:4200")
                  .AllowCredentials()
                   .AllowAnyHeader()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
             services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "QuickStart API",
                    Version = "v1",
                    Description = "QuickStart API by Matech Coding",
                    Contact = new OpenApiContact
                    {
                        Email = "matechcoding@gmail.com",
                        Url = new Uri("https://matechcoding.com"),
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                       new OpenApiSecurityScheme
                    {
                          Reference = new OpenApiReference
                    {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                    },
                           Name = "Bearer",
                    },
                           new List<string>()
                    }
                 });

            });
        }

    }
}
