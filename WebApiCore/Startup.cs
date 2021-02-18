using WebApiCore.Hubs;
using WebApiCore.Models;
using WebApiCore.Options;
using WebApiCore.Services;
using WebApiCore.Shared;
using WebApiCore.Repository;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;

namespace WebApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding signal R
            services.AddSignalR(Options => Options.EnableDetailedErrors = true)
                .AddMessagePackProtocol();

            // add TwilioService
            services.Configure<TwilioSettings>(settings =>
            {
                settings.AccountSid = Configuration.GetSection("Secrets").GetValue<string>("TWILIO_ACCOUNT_SID");
                settings.ApiSecret = Configuration.GetSection("Secrets").GetValue<string>("TWILIO_API_SECRET");
                settings.ApiKey = Configuration.GetSection("Secrets").GetValue<string>("TWILIO_API_KEY");
            });

            services.AddSingleton<TwilioService>();

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();


            //data context connection setup with dapper
            services.AddDbContext<WebAPICoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPractitionerRepository, PractitionerRepository>();

            services.AddResponseCompression(opts =>
               opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                   new[] { "application/octet-stream" }
                   )
               );
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
                endpoints.MapHub<ChatHub>(HubEndpoints.LobbyHub);
                endpoints.MapHub<NotificationHub>(HubEndpoints.NotificationHub);
            });

        }


    }
}
