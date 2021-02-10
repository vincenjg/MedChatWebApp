using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _context;

        public PatientsController(IPatientRepository context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            /* return View(await _context.Patients.ToListAsync());*/
            return View(_context.GetAll());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);*/
            var patient = _context.Find(id.GetValueOrDefault());
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
                /*_context.Add(patient);
                await _context.SaveChangesAsync();*/
                _context.Add(patient);
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
            var patient = _context.Find(id.GetValueOrDefault());
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
                /*try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }                
                }*/
                _context.Update(patient);
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

            /* var patient = await _context.Patients
                 .FirstOrDefaultAsync(m => m.PatientId == id);*/
            _context.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
/*
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);*/
        }

        /*// POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        /*private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }*/
    }
}
