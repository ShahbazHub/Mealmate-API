using System;
using System.Linq;
using System.Text;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Autofac.Core.Lifetime;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Mealmate.Api.Helpers;
using Autofac.Core;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Mealmate.Api
{
    static class ExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc(configure =>
                {
                    configure.EnableEndpointRouting = false;
                    configure.SuppressAsyncSuffixInActionNames = false;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })

                .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, MealmateSettings MealmateSettings)
        {
            // use in-memory database
            //services.AddDbContext<MealmateContext>(c => c.UseInMemoryDatabase("Mealmate"));

            // Add Mealmate DbContext
            // .AddEntityFrameworkSqlServer()
            services
                .AddDbContext<MealmateContext>(options =>
                        options
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .UseSqlServer(MealmateSettings.ConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.MigrationsAssembly("Mealmate.Api");
                            sqlOptions.MigrationsHistoryTable("__MigrationsHistory", "Migration");
                        }
                    ),
                    ServiceLifetime.Transient
                 );

            services.AddTransient<IEmailService, EmailService>();
            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider.GetService<UserManager<User>>();

                if (existingUserManager == null)
                {
                    services.AddIdentity<User, Role>(
                        options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequiredLength = 4;
                            options.Password.RequiredUniqueChars = 0;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;

                            options.User.RequireUniqueEmail = true;
                            //options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                            //    new TokenProviderDescriptor(typeof(CustomEmailConfirmationTokenProvider<IdentityUser>)));
                            //options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                        })
                        .AddEntityFrameworkStores<MealmateContext>()
                        .AddDefaultTokenProviders();
                }
            }
            services.AddTransient<CustomEmailConfirmationTokenProvider<IdentityUser>>();
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mealmate Service",
                    Version = "v1",
                    Description = "Mealmate Service to share the api structure with frontend developers",
                    Contact = new OpenApiContact
                    {
                        Name = "Salman Malik",
                        Email = "salman@askhorizons.com"
                    },
                    TermsOfService = new Uri("https://dev.azure.com/mealmate/MealMate/_git/Mealmate-DotNet")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<MealmateSettings>(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
            services.Configure<AuthMessageSenderOptions>(configuration);
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, MealmateSettings MealmateSettings)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = MealmateSettings.Tokens.Issuer,
                      ValidAudience = MealmateSettings.Tokens.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MealmateSettings.Tokens.Key))
                  };
              })
                .AddGoogle(googleConfig =>
                {
                    googleConfig.ClientId = MealmateSettings.ClientId;
                    googleConfig.ClientSecret = MealmateSettings.ClientSecret;
                });

            return services;
        }

        public static IServiceProvider AddCustomIntegrations(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddHttpContextAccessor();

            var fileProvider = new AppFileProvider(env.ContentRootPath);
            var typeFinder = new WebAppTypeFinder(fileProvider);

            //configure autofac
            var containerBuilder = new ContainerBuilder();

            //register type finder

            containerBuilder.RegisterInstance(fileProvider).As<IAppFileProvider>().SingleInstance();
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //populate Autofac container builder with the set of registered service descriptors
            containerBuilder.Populate(services);

            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();


            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, typeFinder);

            var lifetimeScope = containerBuilder.Build()
                                .BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);

            return new AutofacServiceProvider(lifetimeScope);
        }
    }
}
