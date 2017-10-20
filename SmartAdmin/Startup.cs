using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;
using SmartAdmin.Models;
using SmartAdmin.Services;

namespace SmartAdmin
{
    public class Startup
    {

        private readonly IHostingEnvironment env;
        
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {            
            Configuration = configuration;
            
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartAdminDataContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            
            services.AddIdentity<User, UserRole>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>();
            
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "SmartAdmin";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
                config.ExpireTimeSpan = TimeSpan.FromDays(1);
            });
            
            services.AddPaging();
            
            services.AddLogging();
            
            services.AddMvc(config =>
            {
                if (env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}