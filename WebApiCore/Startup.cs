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
using WebApiCore.Services;
using WebApiCore.Repository;
using Microsoft.AspNetCore.Identity;

namespace WebApiCore
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddNewtonsoftJson();

            //data context connection setup with dapper            
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPractitionerRepository, PractitionerRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddIdentity<Practitioner, PractitionerRoleModel>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<Practitioner>, UserStore>();
            services.AddTransient<IRoleStore<PractitionerRoleModel>, RoleStore>(); 

            services.AddRazorPages();

            //adding signal R
            services.AddSignalR(Options => Options.EnableDetailedErrors = true);

            // add data layer dependencies
            /* var dbConnection = Configuration.GetSection("DefaultConnection");
             services.Configure<ConnectionStrings>(dbConnection);
             services.AddDataAccess();*/
            services.AddMvc();
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
