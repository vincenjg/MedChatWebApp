using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Options;

namespace WebApiCore.Areas.Patients.Views.Lobby
{
    public class PatientLobbyViewModel : PageModel 
    {
        private IHttpClientFactory? httpClientFactory { get; set; }
        private HubConnection _hubConnection { get; set; }

        public List<Practitioner>? Practitioners { get; set; }

        [BindProperty]
        public Practitioner? SelectedPractitioner { get; set; }

        [BindProperty]
        public string LobbyMessage { get; set; }

        public PatientLobbyViewModel(string baseUri)
        {
            _hubConnection = new HubConnectionBuilder()
                .AddMessagePackProtocol()
                .WithUrl(new Uri(baseUri + HubEndpoints.LobbyHub))
                .WithAutomaticReconnect()
                .Build();

            Practitioners = CreatMockPractitionersList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = httpClientFactory.CreateClient("ComponentsClient");
            //hard coded the id for now, but later use patient id from user
            var result = await client.GetAsync("api/Patient/GetAllById?id=1");
            Practitioners = await result.Content.ReadFromJsonAsync<List<Practitioner>>();

            return Page();
        }

        public async Task ConfigureHub()
        {
            _hubConnection.On("AvailabilityChanged", OnGetAsync);
            _hubConnection.On<string>("ReceiveMessage", DisplayLobbyMessage);
            await _hubConnection.StartAsync();
        }

        public void DisplayLobbyMessage(string message)
        {
            LobbyMessage = message;
        }

        private List<Practitioner> CreatMockPractitionersList()
        {
            return new List<Practitioner>()
            {
                new Practitioner{ FirstName="John", LastName="Doe", Title="MD", IsOnline=true},
                new Practitioner{ FirstName="Mary", LastName="Sue", Title="MD", IsOnline=false},
                new Practitioner{ FirstName="Kurnel", LastName="Sanders", Title="MD", IsOnline=false},
                new Practitioner{ FirstName="Otto", LastName="Octavious", Title="OBGYN", IsOnline=true},
                new Practitioner{ FirstName="Mikoto", LastName="Kisaragi", Title="DPM", IsOnline=true},
                new Practitioner{ FirstName="Susan", LastName="Storm", Title="MD", IsOnline=false}
            };
               
        }
    }
}
