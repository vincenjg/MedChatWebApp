using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : Controller
    {
        private readonly ILobbyService _lobbyService;
        private readonly IPractitionerRepository _practitioners;

        public LobbyController(ILobbyService lobbyService, IPractitionerRepository practitioners)
        {
            _lobbyService = lobbyService;
            _practitioners = practitioners;
        }

        [HttpGet(nameof(CreatLobby))]
        public async Task CreatLobby([FromBody] JObject data)
        {
            Practitioner creator = data["creator"].ToObject<Practitioner>();
            string lobbyName = data["lobbyName"].ToString();
            await Task.Run(() => _lobbyService.CreatLobby(creator, lobbyName));
        }

        // TODO: should I make this a post?
        [HttpPost(nameof(JoinLobby))]
        public async Task<string> JoinLobby([FromBody] JObject data)
        {
            string lobbyName = data["lobbyName"].ToString();
            Patient patient = data["patient"].ToObject<Patient>();
            var returnMessage = await Task.Run<string>(() => _lobbyService.JoinLobby(lobbyName, patient));
            return returnMessage;
        }

        [HttpGet(nameof(LeaveLobby))]
        public async Task<string> LeaveLobby([FromBody] JObject data)
        {
            string lobbyName = data["lobbyName"].ToString();
            int patientId = Convert.ToInt32(data["patientId"]);
            var returnMessage = await Task.Run<string>(() => _lobbyService.LeaveLobby(lobbyName, patientId));
            return returnMessage;
        }

        [HttpGet(nameof(DestroyLobby))]
        public async Task DestroyLobby(string lobbyName)
        {
            await Task.Run(() => _lobbyService.DestroyLobby(lobbyName));
        }

        [HttpGet(nameof(GetLobbyMembers))]
        public async Task<List<Patient>> GetLobbyMembers(int id)
        {
            return await Task.Run(() => _lobbyService.GetLobbyMembers(id));
        }

        [HttpPost(nameof(ChangeStatus))]
        public async Task<int> ChangeStatus([FromBody] JObject data)
        {
            //TODO: should I just use the practitioner repository instead of using the lobby service?
            var lobbyName = data["lobbyName"].ToString();
            var status = data["status"].ToObject<PractitionerStatus>();
            var affectedRows = await _practitioners.ChangeStatus(status);
            return affectedRows;
        }
    }
}
