using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Service;
using AutoMapper;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Database.Repository;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using AspNet.Security.OpenIdConnect.Primitives;
using WayVid.Infrastructure.Interfaces.Repository;

namespace WayVid
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddCors(SetCors);
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            services.AddDbContext<ApiDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Local"));
                opt.UseOpenIddict<Guid>();
            });
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddDefaultTokenProviders();

            #region configure auth
            // ASP.NET Core Identity should use the same claim names as OpenIddict
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                options.UseEntityFrameworkCore()
                        .UseDbContext<ApiDbContext>()
                        .ReplaceDefaultEntities<Guid>();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                //options.UseMvc
                // Enable the token endpoint (required to use the password flow).
                options.SetTokenEndpointUris("/connect/token");
                options.SetUserinfoEndpointUris("/connect/userinfo");

                // Allow client applications to use the grant_type=password flow.
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();

                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                //options.RegisterScopes(OpenIddictConstants.Scopes.Email,
                //                       OpenIddictConstants.Scopes.Profile,
                //                       OpenIddictConstants.Scopes.Roles);

                // Accept requests sent by unknown clients (i.e that don't send a client_id).
                // When this option is not used, a client registration must be
                // created for each client using IOpenIddictApplicationManager.
                options.AcceptAnonymousClients();

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                        .EnableAuthorizationEndpointPassthrough() // Add this line.
                        .EnableTokenEndpointPassthrough()
                        .DisableTransportSecurityRequirement(); // During development, you can disable the HTTPS requirement.
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            #endregion

            #region configure DI
            //services.AddIdentityServer().AddApiAuthorization<User>();
            services.AddTransient<IdentityService>();
            services.AddTransient<RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IVisitorService, VisitorService>();
            services.AddTransient<IEstablishmentService, EstablishmentService>();
            services.AddTransient<IOwnerEstablishmentService, OwnerEstablishmentService>();
            services.AddTransient<IRepositoryGeneric<User, ApiDbContext>, UserRepository>();
            services.AddTransient<IRepositoryGeneric<Owner, ApiDbContext>, OwnerRepository>();
            services.AddTransient<IVisitorRepository, VisitorRepository>();
            services.AddTransient<IRepositoryGeneric<Establishment, ApiDbContext>, EstablishmentRepository>();
            services.AddTransient<IVisitLogItemRepository, VisitLogItemRepository>();
            services.AddTransient<IRepositoryGeneric<OwnerEstablishment, ApiDbContext>, RepositoryGeneric<OwnerEstablishment, ApiDbContext>>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
            });
            services.AddAutoMapper((config) =>
            {
                config.AddProfile(new MappingProfile());
            });
            #endregion
        }

        private void SetCors(CorsOptions corsOptions)
        {
            corsOptions.AddPolicy("Default", opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyHeader();
            });
        }

        private void InitializeRoles(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                RoleService roleService = serviceScope.ServiceProvider.GetService<RoleService>();
                roleService.InitializeRoles();
            }
        }

        private void ApplyMigrations(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Console.Write("Start migration... ");
                ApiDbContext context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
                context.Database.Migrate();
                Console.WriteLine("complete");
            }
        }

        private void SetCulture()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetCulture();
            InitializeRoles(app);
            ApplyMigrations(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint($"/{c.RoutePrefix}/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
