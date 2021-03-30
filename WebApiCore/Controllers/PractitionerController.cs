using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Repository;
using Newtonsoft.Json.Linq;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerController : ControllerBase
    {
        private readonly IPatientRepository _patients;
        private readonly IPractitionerRepository _practitioners;

        public PractitionerController(IPatientRepository patients, IPractitionerRepository practitioners)
        {
            _patients = patients;
            _practitioners = practitioners;
        }

        [HttpPost(nameof(Login))]
        public async Task<Practitioner> Login([FromBody] JObject data)
        {
            string email = data["emailAddress"].ToString();
            string password = data["password"].ToString();

            Practitioner practitioner = await _practitioners.Get(email, password);
            return practitioner;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Practitioner> GetById(int id)
        {
            Practitioner practitioner = await _practitioners.Get(id);
            return practitioner;
        }

        // This returns all patients associated with a practitioner.
        [HttpGet("GetAllPatients")]
        public async Task<IEnumerable<Patient>> GetAllById(int id)
        {
            List<Patient> patients = (List<Patient>)await _patients.GetAllById(id);
            return patients;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            int affectedRows = await _practitioners.Delete(id);
            return affectedRows;
        }

        [HttpPost(nameof(Add))]
        public async Task<int> Add(Practitioner practitioner)
        {
            int affectedRows = await _practitioners.Add(practitioner);
            return affectedRows;
        }
    }
}