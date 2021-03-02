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

        [HttpGet("GetTestEnv")]
        public string GetTestEnvVariable()
        {
            string testEnv = _config.GetSection("Secrets").GetValue<string>("TEST_VAR");
            return testEnv;
        }

        [HttpGet("TestRoute")]
        public string TestRoute()
        {
            return "this worked";
        }
    }
}
