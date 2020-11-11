using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCore.Models;


namespace WebApiCore.Controllers
{
    //we add authorize to this controller so they cannot access the site without being authenticated first.
    //so they will be redirected to the login page if they're not signed in.
    //in login.cshtml.cs, an if statement is added so user is redirected to home page if they're authenticated

    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // localhost:44361/Index
        public IActionResult Index()
        {
            return View();
        }

        // localhost:44361/Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // localhost:44361/Home/Home
        public IActionResult Home()
        {
            return View();
        }

        // localhost:44361/Home/Profile
        public IActionResult Profile()
        {
            return View();
        }

        // localhost:44361/Home/Login
        public IActionResult Login()
        {
            return View();
        }

        // localhost:44361/Home/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // localhost:44361/Home/Template
        public IActionResult Template()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
