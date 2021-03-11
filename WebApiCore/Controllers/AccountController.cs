using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Practitioner> UserMgr { get; }
        private SignInManager<Practitioner> SignInMgr { get; }
        public AccountController(UserManager<Practitioner> userManager,
            SignInManager<Practitioner> signInManager)
        {

        }
        /*public IActionResult Index()
        {
            return View();
        }*/
    }
}
