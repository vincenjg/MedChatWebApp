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
using WebApiCore.Repository;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    public class ManageController : Controller
    {
        private readonly IPractitionerRepository _practitioner;
        private readonly IUserService _userService;

        public ManageController(IPractitionerRepository practitioner, IUserService userService)
        {
            _practitioner = practitioner;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userService.GetUserId();
            return View(await _practitioner.GetPractitionerInfo(userId));
        }
    }
}
