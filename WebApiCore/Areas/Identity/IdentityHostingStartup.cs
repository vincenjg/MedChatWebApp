﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiCore.Areas.Identity.Data;
using WebApiCore.Data;

[assembly: HostingStartup(typeof(WebApiCore.Areas.Identity.IdentityHostingStartup))]
namespace WebApiCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebApiCoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TestConnection")));

                services.AddDefaultIdentity<WebApiCoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WebApiCoreContext>();
            });
        }
    }
}