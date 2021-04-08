using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApiCore.Models;
using WebApiCore.Options;
using WebApiCore.Repository;

namespace WebApiCore.Services
{
    public class LobbyService : ILobbyService
    {
        private HttpClient _client;
        private List<Lobby> _lobbies;

        public LobbyService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://ezmedchat.azurewebsites.net/");
            _lobbies = new List<Lobby>();
        }

        public void CreatLobby(Practitioner creator, string lobbyName)
        {
            Lobby lobby = new Lobby
            {
                LobbyName = lobbyName,
                Practitioner = creator,
                Patients = new List<Patient>()
            };

            _lobbies.Add(lobby);
        }

        public string JoinLobby(string lobbyName, Patient patient)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.LobbyName == lobbyName);

            if (lobby == null)
                return "Could not find chosen lobby.";

            lobby.Patients.Add(patient);

            string joinMessage = $"There are now {lobby.Patients.Count} patients in the Lobby.";
            return joinMessage;
        }

        public string LeaveLobby(string lobbyName, int patientId)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.LobbyName == lobbyName);

            if (lobby == null)
                return "Issue leaving lobby.";

            var patient = lobby.Patients.FirstOrDefault(p => p.PatientId == patientId);
            lobby.Patients.Remove(patient);

            string leaveMessage = $"There are now {lobby.Patients.Count} in the Lobby.";
            return leaveMessage;
        }

        public void DestroyLobby(string lobbyName)
        {
            var lobbyToDestroy = _lobbies.FirstOrDefault(l => l.LobbyName == lobbyName);
            _lobbies.Remove(lobbyToDestroy);
        }

        public List<Patient> GetLobbyMembers(int practitionerId)
        {
            var lobby = _lobbies.FirstOrDefault(lobby => lobby.Practitioner.PractitionerID == practitionerId);

            if (lobby == null || lobby.Patients == null)
            {
                return new List<Patient>();
            }

            return lobby.Patients;
        }

        public async Task<int> ChangeStatus(string lobbyName, PractitionerStatus status)
        {
            //TODO: should I keep the lobbyService a singleton? If so, is using a HttpClient the best way to do this?
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Practitioner/ChangeStatus", content);
            var affectedRows = await response.Content.ReadFromJsonAsync<int>();
            return affectedRows;
        }
    }
}
