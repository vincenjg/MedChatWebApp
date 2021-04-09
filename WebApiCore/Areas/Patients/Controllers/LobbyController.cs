using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;
using WebApiCore.Services;

namespace WebApiCore.Areas.Patients.Controllers
{
    [Area("Patients")]
    public class LobbyController : Controller
    {
        private readonly IPatientRepository _patients;
        private readonly IPractitionerRepository _practitioners;
        private readonly ILobbyService _lobbyService;

        [BindProperty]
        public PatientLobbyViewModel patientLobbyVM { get; set; }

        public LobbyController(IPatientRepository patients,IPractitionerRepository practitioners, ILobbyService lobbyService)
        {
            _patients = patients;
            _practitioners = practitioners;
            _lobbyService = lobbyService;
        }
        [Authorize(Roles = "PATIENT")]
        public async Task<IActionResult> IndexAsync()
        {
            patientLobbyVM = new PatientLobbyViewModel()
            {
                Practitioners = (List<Practitioner>)await _practitioners.GetAllById(1)
            };

            return View(patientLobbyVM);
        }

        public async Task<IActionResult> GetPractitionersAsync(int id)
        {
            patientLobbyVM = new PatientLobbyViewModel()
            {
                Practitioners = (List<Practitioner>)await _practitioners.GetAllById(1)
            };

            return PartialView("~/Views/Shared/_Cards.cshtml", patientLobbyVM);
        }

        [HttpPost]
        public async Task<string> JoinLobby([FromBody] JObject data)
        {
            string lobbyName = data["lobbyName"].ToString();
            int id = Convert.ToInt32(data["id"]);

            Patient patient = await _patients.Get(id);
            var message = _lobbyService.JoinLobby(lobbyName, patient);
            return message;
        }

        [HttpPost]
        public string LeaveLobby([FromBody] JObject data)
        {
            string lobbyName = data["lobbyName"].ToString();
            int id = Convert.ToInt32(data["id"]);

            var message = _lobbyService.LeaveLobby(lobbyName, id);
            return message;
        }
    }
}
