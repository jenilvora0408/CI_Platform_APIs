using API.ExtAuthorization;
using API.Middleware;
using Common.Constants;
using Common.Utils.Models;
using DataAccessLayer;
using DataAccessLayer.Repositories.Implementations;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Implementations;
using Services.Interfaces;
using Services.Profiles;
using System.Reflection;

namespace API.Extension;

public static class BuilderConfig
{
    public static void DbConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
            builder.Configuration.GetConnectionString(SystemConstant.CONNECTION_STRING_NAME)
        ));
        builder.Services.AddDbContext<ApplicationDbContext>();
    }

    public static void SwaggerConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(SwaggerConstants.NAME, new OpenApiInfo { Title = SwaggerConstants.TITLE, Version = SwaggerConstants.VERSION });
            c.AddSecurityDefinition(SwaggerConstants.SECURITY_SCHEME, new OpenApiSecurityScheme()
            {
                Name = SwaggerConstants.SECURITY_SCHEME_NAME,
                Type = SecuritySchemeType.ApiKey,
                Scheme = SwaggerConstants.SECURITY_SCHEME,
                BearerFormat = SwaggerConstants.SECURITY_SCHEME_FORMAT,
                In = ParameterLocation.Header,
                Description = SwaggerConstants.SECURITY_SCHEME_DESCRIPTION
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerConstants.SECURITY_SCHEME
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void ExtAuthConfig(this WebApplicationBuilder builder, JwtSetting jwtSetting, EmailSetting emailSetting)
    {
        builder.Configuration.GetSection(SystemConstant.JWT_SETTING).Bind(jwtSetting);
        builder.Configuration.GetSection(SystemConstant.EMAIL_SETTING).Bind(emailSetting);

        // Custom Auth filter
        builder.Services.AddScoped<ExtAuthorizeFilter>();

        // Register the ExtAuthorizeHandler before adding policies
        builder.Services.AddSingleton<IAuthorizationHandler, ExtAuthorizeHandler>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(SystemConstant.ADMIN_POLICY, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstant.ADMIN_POLICY));
            });

            options.AddPolicy(SystemConstant.VOLUNTEER_POLICY, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstant.VOLUNTEER_POLICY));
            });
        });

    }

    public static void ServiceAndRepoConfig(this WebApplicationBuilder builder)
    {
        // Add Repo scoped
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add Service scoped
        RegisterServicesImplementingInterface(typeof(IBaseService<>), builder.Services);

        builder.Services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(ICityService))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
    }

    //Handles the automatic registration of services that implement the specified generic interface.
    private static void RegisterServicesImplementingInterface(Type interfaceType, IServiceCollection services)
    {
        IEnumerable<Type> implementationTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));

        foreach (Type implementationType in implementationTypes)
        {
            Type[] implementedInterfaces = implementationType.GetInterfaces();
            foreach (Type implementedInterface in implementedInterfaces)
            {
                if (implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() == interfaceType)
                {
                    services.AddScoped(implementedInterface, implementationType);
                }
            }
        }
    }

    public static void MiddlewareConfig(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddTransient<ExceptionMiddleware>();
    }

    public static void InternalServicesConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(SystemConstant.CORS_ALLOW_ANY, option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyMethod();
                option.AllowAnyHeader();
            });
        });

        // Handle custom model validation exception
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Route Lowercase
        builder.Services.AddRouting(option => option.LowercaseUrls = true);

        // Add controllers and configure endpoints
        builder.Services.AddControllers().AddNewtonsoftJson();

        //IMemoryCache
        builder.Services.AddMemoryCache();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddEndpointsApiExplorer();
    }

    public static void ExternalServiceConfig(this WebApplicationBuilder builder)
    {
        // Adding Mapper
        builder.Services.AddAutoMapper(typeof(MapProfile));
    }
}
