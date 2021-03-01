using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Controllers
{
    public class VideoCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
