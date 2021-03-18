using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

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

        // localhost:44361/Home/Profile
        public IActionResult UserProfile()
        {
            return View();
        }


        // localhost:44361/Home/Template
        public IActionResult Template()
        {
            return View();
        }

        // localhost:44361/Home/Status
        public IActionResult Status()
        {
            return View();
        }

        // localhost:44361/Home/WaitingRoom
        public IActionResult WaitingRoom()
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
