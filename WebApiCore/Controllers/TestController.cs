using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TestController : Controller
    {
        private readonly IConfiguration _config;

        public TestController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("GetTestConnString")]
        public string GetTestConnectionString()
        {
            string testString = _config.GetConnectionString("TestString");
            return testString;
        }

        [HttpGet("GetAzureConnString")]
        public string GetTestEnvVariable()
        {
            string azureConnString = _config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(azureConnString))
            {
                return "could not retrieve azureConnString from azure";
            }

            return "retrieved azureConnString from azure";
        }

        [HttpGet("TesTwilioSecrets")]
        public string TestRoute()
        {
            string twilio1 = _config.GetValue<string>("Secrets:TwilioAccountSid");

            if (string.IsNullOrEmpty(twilio1))
            {
                return "could not retrieve twilio secrets from azure";
            }

            return "retrieved twilio secrets from azure";
        }
    }
}
