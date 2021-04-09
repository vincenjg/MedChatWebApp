using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Controllers
{
    public class VideoCallController : Controller
    {
        [Authorize(Roles = "PATIENT,PRAC")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
