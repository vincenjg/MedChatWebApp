using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Services
{
    public interface ILobbyService
    {
        public void CreatLobby(Practitioner creator, string lobbyName);
        public string JoinLobby(string lobbyName, Patient patient);
        public string LeaveLobby(string lobbyName, int lobbyCount);
        public void DestroyLobby(string lobbyName);
        public List<Patient> GetLobbyMembers(int id);
        public Task<int> ChangeStatus(string lobbyName, PractitionerStatus status);
    }
}
