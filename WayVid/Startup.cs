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
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Local")));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApiDbContext>();
            services.AddTransient<IdentityService>();
            services.AddTransient<RoleService>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
            });
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


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint($"/{c.RoutePrefix}/v1/swagger.json", "My API V1");
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
