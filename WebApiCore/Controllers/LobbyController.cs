using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : Controller
    {
        private readonly ILobbyService _lobbyService;

        public LobbyController(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        [HttpGet(nameof(CreatLobby))]
        public async Task CreatLobby(Practitioner creator, string lobbyName)
        {
            await Task.Run(() => _lobbyService.CreatLobby(creator, lobbyName));
        }

        [HttpGet(nameof(JoinLobby))]
        public async Task<string> JoinLobby(string lobbyName, Patient patient)
        {
            var returnMessage = await Task.Run<string>(() => _lobbyService.JoinLobby(lobbyName, patient));
            return returnMessage;
        }

        [HttpGet(nameof(LeaveLobby))]
        public async Task<string> LeaveLobby(string lobbyName, int patientId)
        {
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
            var lobbyName = data["lobbyName"].ToString();
            var status = data["status"].ToObject<PractitionerStatus>();
            var affectedRows = await _lobbyService.ChangeStatus(lobbyName, status);
            return affectedRows;
        }
    }
}
