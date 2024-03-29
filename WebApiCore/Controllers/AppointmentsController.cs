﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointments;

        public AppointmentsController(IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Appointment> GetById(int id)
        {
            Appointment appointment = await _appointments.Get(id);
            return appointment;
        }


        [HttpGet(nameof(GetAllPatientsById))]
        public async Task<IEnumerable<Appointment>> GetAllPatientsById(string id)
        {
            List<Appointment> appointments = (List<Appointment>)await _appointments.GetAllByPatientId(id);
            return appointments;
        }

        [HttpGet(nameof(GetAllPractitionerById))]
        public async Task<IEnumerable<Appointment>> GetAllPractitionerById(string id)
        {
            List<Appointment> appointments = (List<Appointment>)await _appointments.GetAllByPractitionerId(id);
            return appointments;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            int affectedRows = await _appointments.Delete(id);
            return affectedRows;
        }

        [HttpPost(nameof(Add))]
        public async Task<int> Add(Appointment appointment)
        {
            int affectedRows = await _appointments.Add(appointment);
            return affectedRows;
        }
    }
}
