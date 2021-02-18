﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    //this is a test view. 
    public class PractitionerView : Controller
    {
        private readonly IPractitionerRepository _practitioner;

        public PractitionerView(IPractitionerRepository practitioner)
        {
            _practitioner = practitioner;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _practitioner.GetAll());
        }
    }
}