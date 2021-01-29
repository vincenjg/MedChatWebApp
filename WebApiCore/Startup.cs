using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiCore.Hubs;
using WebApiCore.Models;
using WebApiCore.Utilities;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Services;

namespace WebApiCore
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
            services.AddControllersWithViews();

            //data context connection setup with dapper
            services.AddDbContext<WebAPICoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TestConnection")));
            services.AddScoped<IDapper, Dapperr>();

            //to be added
            //added this part after setting up the registration
            services.AddRazorPages();

            //adding signal R
            services.AddSignalR();

            // add data layer dependencies
           /* var dbConnection = Configuration.GetSection("TestConnection");
            services.Configure<ConnectionStrings>(dbConnection);
            services.AddDataAccess();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //registration
            //to be added
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //to be added ... this is for URLs
                endpoints.MapRazorPages();

                //for signalR - a route for the Hub that our clients will connect to (right before UseMvc):
                endpoints.MapHub<ChatHub>("/chathub");
            });

        }


    }
}
