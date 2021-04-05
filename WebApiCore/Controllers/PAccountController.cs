using WebApiCore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Controllers
{
    public class PAccountController : Controller
    {
        public IPractitionerRepository _practitioners;

        public PAccountController(IPractitionerRepository practitioners)
        {
            _practitioners = practitioners;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
