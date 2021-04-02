using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiCore.Options
{
    public class ComponentHttpClient : HttpClient
    {
        public ComponentHttpClient()
        {
            this.BaseAddress = new Uri("https://ezmedchat.azurewebsites.net/");
        }
    }
}
