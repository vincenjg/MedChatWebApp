using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Areas.Patients.Views.Lobby;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Areas.Patients.Controllers
{
    [Area("Patients")]
    public class LobbyController : Controller
    {
        private readonly IPractitionerRepository _practitionerRepository;

        [BindProperty]
        public PatientLobbyViewModel patientLobbyVM { get; set; }

        public LobbyController(IPractitionerRepository practitionerRepository)
        {
            _practitionerRepository = practitionerRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            patientLobbyVM = new PatientLobbyViewModel("https://localhost:44361/")
            {
                //Practitioners = (List<Practitioner>)await _practitionerRepository.GetAllById(1)
            };

            await patientLobbyVM.ConfigureHub();

            return View(patientLobbyVM);
        }
    }
}
