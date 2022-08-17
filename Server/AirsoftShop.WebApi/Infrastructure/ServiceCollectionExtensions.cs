namespace AirsoftShop.WebApi.Infrastructure;

using System.Text;
using AirsoftShop.Data.Models;
using Data.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AirsoftShop.Common.Models;
using Common.Services;
using Services.Services.Category;
using Services.Services.Identity;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }

    internal static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    internal static JwtConfiguration GetJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingsConfiguration = configuration.GetSection("JwtConfiguration");
        services.Configure<JwtConfiguration>(jwtSettingsConfiguration);

        var appSettings = jwtSettingsConfiguration.Get<JwtConfiguration>();

        return appSettings;
    }

    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        JwtConfiguration jwtConfiguration)
    {
        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    internal static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        => services.AddScoped<ICurrentUserService, CurrentUserService>()
            .AddTransient<IIdentityService, IdentityService>()
            .AddTransient<ICategoryService, CategoryService>();
}