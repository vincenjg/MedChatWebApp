﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Models;
using WebApiCore.Repository;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    public class PatientsViewController : Controller
    {
        private readonly IPatientRepository _patients;
        private readonly IUserService _userService;
        private readonly IPractitionerRepository _practitionerRepository;

        public PatientsViewController(IPatientRepository patients, IUserService userService, IPractitionerRepository practitionerRepository)
        {
            _patients = patients;
            _userService = userService;
            _practitionerRepository = practitionerRepository;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            //return View(await _patients.GetAll());

            var userId = _userService.GetUserId();
            return View(await _patients.GetAllById(userId));
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _patients.Get(id.GetValueOrDefault());
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,EpicId,FirstName,LastName,EmailAddress,TestPassword")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _patients.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var patient = await _context.Patients.FindAsync(id);*/
            var patient = await _patients.Get(id.GetValueOrDefault());
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,EpicId,FirstName,LastName,EmailAddress,TestPassword")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

            await _patients.Update(patient);
            return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _patients.Delete(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));

        }

    }
}
