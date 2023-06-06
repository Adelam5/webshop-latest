using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Repositories.Commands;
using Application.Common.Interfaces.Repositories.Queries;
using Application.Common.Interfaces.Services;
using Domain.Abstractions.Interfaces;
using Domain.Abstractions.Interfaces.Repositories;
using Infrastructure.Authentication;
using Infrastructure.BackgroundJobs;
using Infrastructure.Idempotence;
using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
using Infrastructure.Interceptors;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Commands;
using Infrastructure.Services;
using Infrastructure.Services.Cache;
using Infrastructure.Services.Email;
using Infrastructure.Services.Payment;
using Infrastructure.Services.S3;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        services.AddAutoMapper(InfrastructureAssembly.Instance);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddDistributedMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddScoped<ICartRepository, CartRepository>();



        var sendGridSettings = new SendGridSettings();
        configuration.Bind(SendGridSettings.SectionName, sendGridSettings);
        if (env.EnvironmentName == Environments.Production)
        {
            sendGridSettings.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")!;
        }
        services.AddSingleton(Options.Create(sendGridSettings));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailSender, EmailSender>();

        var s3Settings = new S3Settings();
        configuration.Bind(S3Settings.SectionName, s3Settings);
        if (env.EnvironmentName == Environments.Production)
        {
            s3Settings.Secret = Environment.GetEnvironmentVariable("AWS_S3_SECRET")!;
            s3Settings.Key = Environment.GetEnvironmentVariable("AWS_S3_KEY")!;
        }
        services.AddSingleton(Options.Create(s3Settings));
        services.AddScoped<IS3Service, S3Service>();

        var stripeSettings = new StripeSettings();
        configuration.Bind(StripeSettings.SectionName, stripeSettings);
        if (env.EnvironmentName == Environments.Production)
        {
            stripeSettings.SecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY")!;
            stripeSettings.PublishableKey = Environment.GetEnvironmentVariable("STRIPE_PUBLISHABLE_KEY")!;
        }
        services.AddSingleton(Options.Create(stripeSettings));
        services.AddScoped<IPaymentService, PaymentService>();

        services
            .AddPersistance(configuration)
            .AddIdentity(configuration)
            .AddAuth(configuration, env)
            .AddQuartzConfiguration();

        return services;
    }

    public static IServiceCollection AddQuartzConfiguration(
    this IServiceCollection services)
    {
        services.AddScoped<IJob, ProcessOutboxMessagesJob>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(60)
                                .RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<InvalidateCachedDataInterceptor>();

        services.AddDbContext<DataContext>((serviceProvider, options) =>
        {
            var outboxInterceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;
            var auditableInterceptor = serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>()!;
            var cacheableInterceptor = serviceProvider.GetService<InvalidateCachedDataInterceptor>()!;

            options.UseNpgsql(
                        ConnectionHelper.GetConnectionString(configuration))
                   .UseSnakeCaseNamingConvention()
                   .AddInterceptors(
                        outboxInterceptor,
                        auditableInterceptor,
                        cacheableInterceptor);
        });

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddScoped<DbConnectionFactory>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductCommandsRepository, ProductCommandsRepository>();
        services.AddScoped<IProductQueriesRepository, ProductQueriesRepository>();
        services.AddScoped<ICustomerCommandsRepository, CustomerCommandsRepository>();
        services.AddScoped<ICustomerQueriesRepository, CustomerQueriesRepository>();
        services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();
        services.AddScoped<IOrderCommandsRepository, OrderCommandsRepository>();
        services.AddScoped<IOrderQueriesRepository, OrderQueriesRepository>();

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDataContext>(options =>
            options
            .UseNpgsql(ConnectionHelper.GetConnectionString(configuration))
            .UseSnakeCaseNamingConvention());

        services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

        var passwordOptions = new PasswordOptions()
        {
            RequireDigit = false,
            RequireLowercase = false,
            RequiredLength = 4,
            RequireNonAlphanumeric = false,
            RequireUppercase = false,
        };

        services.Configure<IdentityOptions>(options =>
        {
            options.Password = passwordOptions;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services,
           IConfiguration configuration, IWebHostEnvironment env)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        if (env.EnvironmentName == Environments.Production)
        {
            jwtSettings.Secret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        }
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<ICookieService, CookieService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddCookie(options =>
            {
                options.Cookie.Name = "token";
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidIssuer = jwtSettings.Issuer,
                   ValidAudience = jwtSettings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
               };
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context =>
                   {
                       context.Token = context.Request.Cookies["token"];
                       return Task.CompletedTask;
                   }
               };
           });

        return services;
    }
}
