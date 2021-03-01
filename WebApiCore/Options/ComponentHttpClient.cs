using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiCore.Options
{
    public class ComponentHttpClient : HttpClient
    {
        public HttpClient HttpClient;
        public NavigationManager NavigationManager;
        public ComponentHttpClient(HttpClient httpClient, NavigationManager navigationManager)
        {
            HttpClient = httpClient;
            NavigationManager = navigationManager;
            HttpClient.BaseAddress = new Uri(NavigationManager.BaseUri);
        }
    }
}
