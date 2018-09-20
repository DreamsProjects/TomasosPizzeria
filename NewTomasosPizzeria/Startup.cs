using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewTomasosPizzeria.Models;

namespace NewTomasosPizzeria
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<NyTomasosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSession();
            services.AddDistributedMemoryCache();


            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.HttpOnly = true;
                option.Cookie.Expiration = TimeSpan.FromDays(10);
                option.LoginPath = "/Kunds/Login";
                option.LogoutPath = "/Kunds/Logout";
                option.SlidingExpiration = true;
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env) 
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=MatrattProdukts}/{action=Welcome}/{id?}");
            });
        }
    }
}
