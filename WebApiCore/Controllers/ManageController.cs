using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiCore.Models;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
