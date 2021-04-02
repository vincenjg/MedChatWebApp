using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;
using WebApiCore.Services;

namespace WebApiCore.Areas.Practitoners.Contollers
{
    [Area("Practitioners")]
    public class LobbyController : Controller
    {
        private readonly IPractitionerRepository _practitioners;
        private readonly ILobbyService _lobbyService;
        private readonly IUserService _userService;

        public LobbyController(IPractitionerRepository practitioners, ILobbyService lobbyService, IUserService userService)
        {
            _practitioners = practitioners;
            _lobbyService = lobbyService;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            int userId = Convert.ToInt32(_userService.GetUserId());
            Practitioner user = await _practitioners.Get(userId);

            var patients = user.IsOnline ?
                _lobbyService.GetLobbyMembers(userId) : new List<Patient>();

            PractitionerLobbyViewModel practitionerLobbyVM = 
                new PractitionerLobbyViewModel()
                {
                    Patients = patients,
                    IsOnline = user.IsOnline
                };

            return View(practitionerLobbyVM);
        }

        [HttpGet]
        public IActionResult GetLobbyMembers(bool isOnline)
        {
            int userId = Convert.ToInt32(_userService.GetUserId());

            PractitionerLobbyViewModel practitionerLobbyVM =
               new PractitionerLobbyViewModel()
               {
                    Patients = _lobbyService.GetLobbyMembers(userId),
                    IsOnline = isOnline
               };

            return PartialView("~/Views/Shared/_LobbyMembers.cshtml", practitionerLobbyVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromBody] JObject data)
        {
            PractitionerStatus status = data["status"].ToObject<PractitionerStatus>();
            //TODO: do some error checking here if I have time.
            var affectedRows = await _practitioners.ChangeStatus(status);

            if (status.isOnline)
            {
                await CreateLobby();

                PractitionerLobbyViewModel practitionerLobbyVM =
                new PractitionerLobbyViewModel()
                {
                    //Patients = _lobbyService.GetLobbyMembers(status.id),
                    Patients = null,
                    IsOnline = status.isOnline
                };

                return PartialView("~/Views/Shared/_LobbyMembers.cshtml", practitionerLobbyVM);
            }
            else
            {
                await DestroyLobby();

                PractitionerLobbyViewModel practitionerLobbyVM =
                   new PractitionerLobbyViewModel()
                   {
                       IsOnline = status.isOnline
                   };

                return PartialView("~/Views/Shared/_LobbyMembers.cshtml", practitionerLobbyVM);
            }

        }

        private async Task CreateLobby()
        {
            int userId = Convert.ToInt32(_userService.GetUserId());
            Practitioner user = await _practitioners.Get(userId);
            _lobbyService.CreatLobby(user, user.FullName);
        }

        private async Task DestroyLobby()
        {
            int userId = Convert.ToInt32(_userService.GetUserId());
            Practitioner user = await _practitioners.Get(userId);
            _lobbyService.DestroyLobby(user.FullName);
        }
    }
}
