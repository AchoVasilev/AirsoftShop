namespace AirsoftShop.WebApi.Infrastructure;

using System.Text;
using AirsoftShop.Data.Models;
using Data.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AirsoftShop.Common.Models;
using CloudinaryDotNet;
using Common.Services;
using Microsoft.OpenApi.Models;
using Models;
using Services.Common;
using Services.Common.Factories;

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
        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret!);

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

    internal static IServiceCollection RegisterCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        var cloudinarySection = configuration.GetSection("Cloudinary");
        
        services.Configure<CloudinarySettings>(cloudinarySection);
        var appSettings = cloudinarySection.Get<CloudinarySettings>();

        var cloudinaryAccount = new Account(appSettings.CloudifyName, appSettings.ApiKey, appSettings.ApiSecret);
        var cloudinary = new Cloudinary(cloudinaryAccount);
        services.AddSingleton(cloudinary);

        return services;
    }

    internal static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        var transientRegistrationType = typeof(ITransientService);
        var scopedRegistrationType = typeof(IScopedService);
        var singletonRegistrationType = typeof(ISingletonService);

        var types = transientRegistrationType
            .Assembly
            .GetExportedTypes()
            .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Services = t.GetInterfaces().ToList(),
                Implementation = t
            })
            .Where(s => s.Services.Count > 0);

        foreach (var type in types)
        {
            foreach (var service in type.Services)
            {
                if (transientRegistrationType.IsAssignableFrom(service))
                {
                    services.AddTransient(service, type.Implementation);
                }
                else if (scopedRegistrationType.IsAssignableFrom(service))
                {
                    services.AddScoped(service, type.Implementation);
                }
                else if (singletonRegistrationType.IsAssignableFrom(service))
                {
                    services.AddSingleton(service, type.Implementation);
                }
            }
        }

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IProductFactory<Gun, ProductResultModel>, GunFactory>();

        return services;
    }

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "AirsoftShop API", Version = "v1" });
        });
}