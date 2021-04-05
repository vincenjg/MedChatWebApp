using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.KeyVault;
using WebApiCore.Options;

namespace WebApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

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
                    var defaultAzureCredentialOptions = new DefaultAzureCredentialOptions();
                    defaultAzureCredentialOptions.ExcludeAzureCliCredential = false;
                    defaultAzureCredentialOptions.ExcludeEnvironmentCredential = true;
                    defaultAzureCredentialOptions.ExcludeInteractiveBrowserCredential = true;
                    defaultAzureCredentialOptions.ExcludeManagedIdentityCredential = true;
                    defaultAzureCredentialOptions.ExcludeSharedTokenCacheCredential = true;
                    defaultAzureCredentialOptions.ExcludeVisualStudioCodeCredential = true;
                    defaultAzureCredentialOptions.ExcludeVisualStudioCredential = true;

                    var credential = new DefaultAzureCredential(defaultAzureCredentialOptions);
                    var token = credential.GetToken(
                        new Azure.Core.TokenRequestContext(
                            new[] { "https://vault.azure.net/.default" }));
                    return token.Token;
                });
                config.AddAzureKeyVault(vaultName, keyVaultClient, new PrefixKeyVaultSecretManager("EZMedChat"));
            });
    }
}
