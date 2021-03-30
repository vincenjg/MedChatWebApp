using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApiCore.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IConfiguration _config;

        public UserLoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Practitioner pracLogin)
        {
            
            return View();
        }
    }
}
