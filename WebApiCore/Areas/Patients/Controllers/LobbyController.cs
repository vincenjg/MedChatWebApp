using Microsoft.AspNetCore.Mvc;
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
        private readonly IPractitionerRepository _practitionerRepository;
        private readonly ILobbyService _lobbyService;

        [BindProperty]
        public PatientLobbyViewModel patientLobbyVM { get; set; }

        public LobbyController(IPractitionerRepository practitionerRepository, ILobbyService lobbyService)
        {
            _practitionerRepository = practitionerRepository;
            _lobbyService = lobbyService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            patientLobbyVM = new PatientLobbyViewModel()
            {
                Practitioners = (List<Practitioner>)await _practitionerRepository.GetAllById(1)
            };

            return View(patientLobbyVM);
        }

        public async Task<IActionResult> GetPractitionersAsync(int id)
        {
            patientLobbyVM = new PatientLobbyViewModel()
            {
                Practitioners = (List<Practitioner>)await _practitionerRepository.GetAllById(1)
            };

            return PartialView("~/Views/Shared/_Cards.cshtml", patientLobbyVM);
        }

        [HttpPost]
        public async Task<string> JoinLobby(string lobbyName, Patient patient)
        {
            var message = await Task.Run(() => _lobbyService.JoinLobby(lobbyName, patient));
            return message;
        }
    }
}
