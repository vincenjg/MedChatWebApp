using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Areas.Practitoners.Views.Lobby;

namespace WebApiCore.Areas.Practitoners.Contollers
{
    [Area("Practitioners")]
    public class LobbyController : Controller
    {
        [BindProperty]
        public PractitionerLobbyViewModel practitionerLobbyVM { get; set; }

        public LobbyController()
        {

        }

        public async Task<IActionResult> IndexAsync()
        {
            practitionerLobbyVM = new PractitionerLobbyViewModel("https://localhost:44361/")
            { 
                
            };

            await practitionerLobbyVM.ConfigureHub();

            return View(practitionerLobbyVM);
        }
    }
}
