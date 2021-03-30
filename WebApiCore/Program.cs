using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.KeyVault;
using WebApiCore.Options;

namespace WebApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            /*BuildWebHost(args).Run();*/
        }

 /*       public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();*/

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var settings = config.Build();
                var vaultName = settings["KeyVault:VaultName"];

                var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
                {
                    var credential = new DefaultAzureCredential(false);
                    var token = credential.GetToken(
                        new Azure.Core.TokenRequestContext(
                            new[] { "https://vault.azure.net/.default" }));
                    return token.Token;
                });
                config.AddAzureKeyVault(vaultName, keyVaultClient, new PrefixKeyVaultSecretManager("EZMedChat"));
            });
    }
}
