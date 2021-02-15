using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patients;
        private readonly IPractitionerRepository _practitioners;

        public PatientController(IPatientRepository patients, IPractitionerRepository practitioners)
        {
            _patients = patients;
            _practitioners = practitioners;
        }

        [HttpPost(nameof(Login))]
        public async Task<Patient> Login([FromBody] JObject data)
        {
            string email = data["emailAddress"].ToString();
            string password = data["password"].ToString();

            Patient patient = await _patients.Get(email, password);
            return patient;
        }
        //using nameof for consistency

        //[HttpGet("Get/{id}")]
        //https://localhost:44361/api/Patient/Get/1       
        [HttpGet(nameof(GetById))]
        public async Task<Patient> GetById(int id)
        {
            Patient patient = await _patients.Get(id);
            return patient;
        }

        // This returns all practitioners associated with a patient.
        //https://localhost:44361/api/Patient/GetAllPractitioners/2
        //[HttpGet("GetAllPractitioners/{id}")]

        [HttpGet(nameof(GetAllById))] 
        //https://localhost:44361/api/Patient/GetAllById?id=2
        public async Task<IEnumerable<Practitioner>> GetAllById(int id)
        {
            List<Practitioner> practitioners = (List<Practitioner>)await _practitioners.GetAllById(id);
            return practitioners;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            int affectedRows = await _patients.Delete(id);
            return affectedRows;
        }

        [HttpPost(nameof(Add))]
        public async Task<int> Add(Patient patient)
        {
            int affectedRows = await _patients.Add(patient);
            return affectedRows;
        }
    }
}