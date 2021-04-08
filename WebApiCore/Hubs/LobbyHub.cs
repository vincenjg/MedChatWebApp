using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task ChangeStatus(string lobbyName, PractitionerStatus status)
        {
            await Clients.Group(lobbyName).SendAsync("AvailabilityChanged", status);
        }

        public async Task CreateLobby(string lobbyName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);
        }

        public async Task JoinLobby(string lobbyName, string lobbyMessage)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);
            await Clients.Group(lobbyName).SendAsync("ReceiveMessage", lobbyMessage);
        }

        public async Task LeaveLobby(string lobbyName, string lobbyMessage)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyName);
            await Clients.Group(lobbyName).SendAsync("ReceiveMessage", lobbyMessage);
        }

        public async Task DestroyLobby(string lobbyName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyName);
        }
    }
}
