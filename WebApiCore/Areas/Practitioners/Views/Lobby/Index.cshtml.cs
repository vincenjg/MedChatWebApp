using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Options;

namespace WebApiCore.Areas.Practitoners.Views.Lobby
{
    public class PractitionerLobbyViewModel : PageModel
    {
        private HubConnection? _hubConnection { get; set; }

        protected IHttpClientFactory _clientFactory { get; set; } = null!;

        public List<Patient> Patients { get; set; }

        private bool IsOnline = false;

        public PractitionerLobbyViewModel(string baseUri)
        {
            _hubConnection = new HubConnectionBuilder()
                .AddMessagePackProtocol()
                .WithUrl(new Uri(baseUri + HubEndpoints.LobbyHub))
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task<IActionResult> OnGetLobbyMembersAsync()
        {
            var client = _clientFactory.CreateClient("ComponentsClient");
            var result = await client.GetFromJsonAsync<List<Patient>>("api/Practitioner/GetAllPatients?id=1");
            Patients = result;

            return Page();
        }

        public async Task UpdateLobbyPatientList()
        {
           var client = _clientFactory.CreateClient("ComponentsClient");
           var lobbyMembers = await client.GetFromJsonAsync<List<Patient>>("api/Lobby/GetLobbyMembers?id=1");

           Patients = lobbyMembers;
        }
        
         public async Task ChangeStatus()
         {
            // send out status change
            IsOnline = !IsOnline;

            // use practitioner name
            string lobbyName = "lobby name";
            PractitionerStatus status = new PractitionerStatus
            {
                id = 1,
                isOnline = IsOnline
            };

            var content = new StringContent(JsonConvert.SerializeObject(new { lobbyName, status}), Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient("ComponentsClient");

            await client.PostAsync("api/lobby/changestatus", content);
            await _hubConnection.InvokeAsync(HubEndpoints.ChangeStatus, lobbyName, status);

            // create lobby if status is changed to online or destroy it if status is changed to offline.
            if (IsOnline)
            {
                await CreateLobby();
            }
            else
            {
                await DestroyLobby();
            }
         }

        public async Task CreateLobby()
        {
            await _hubConnection.InvokeAsync(HubEndpoints.CreatLobby, "lobby name");
        }

        public async Task DestroyLobby()
        {
            await _hubConnection.InvokeAsync(HubEndpoints.DestroyLobby, "lobby name");
        }

        public async Task ConfigureHub()
        {
            _hubConnection.On(HubEndpoints.JoinLobby, UpdateLobbyPatientList);
            await _hubConnection.StartAsync();
        }
    }
}
