using EmailService.Config;
using EmailService.Contracts;
using EmailService.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using QuickStart;
using QuickStart.Extensions;
using QuickStart.Presentation.ActionFilters;
using Service.JwtFeatures;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailResetPassword, EmailResetPassword>();
builder.Services.AddScoped<IEmailConfirm, EmailConfirm>();
builder.Services.AddScoped<IEmailNotification, EmailNotification>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddScoped<JwtHandler>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(QuickStart.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

app.UseExceptionHandler(opt => { });

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Matech Coding API v1");
});

app.MapControllers();

app.MapFallbackToController("Index", "Fallback");

app.Run();
