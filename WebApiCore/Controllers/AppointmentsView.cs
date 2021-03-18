using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    public class AppointmentsView : Controller
    {

        private readonly IAppointmentRepository _appointments;

        public AppointmentsView(IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        //view all appointments. 
        public async Task<IActionResult> Index()
        {
            return View(await _appointments.GetAll());
        }

        //test to view only appointments based on a practitioner's ID
        public async Task<IActionResult> GetPrac()
        {
            return View(await _appointments.GetAllByPractitionerId(5));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointments.Get(id.GetValueOrDefault());
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentsView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,StartTime,EndTime,AppointmentReason,AppointmentInstructions,PatientID,PractitionerID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointments.Add(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: AppointmentsView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointments.Get(id.GetValueOrDefault());
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,StartTime,EndTime,AppointmentReason,AppointmentInstructions,PatientID,PractitionerID")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _appointments.Update(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: AppointmentsView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _appointments.Delete(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));

        }

    }
}
