using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApiCore.Models;

namespace WebApiCore.Hubs
{
    public class ChatHub : Hub
    {
        //TODO: handle what to do on disconnections.

        // stores group name and connection mappings for each group.
        public List<LobbyGroup> groups = new List<LobbyGroup>();

        public async Task ChangeStatus(string groupName, int practitionerId, bool isOnline)
        {
            PractitionerStatus status = new PractitionerStatus { id = practitionerId, isOnline = isOnline };
            await Clients.Group(groupName).SendAsync("AvailabilityChanged", status);
        }
        
        public async Task CreateGroup(string groupName)
        {
            // add new container for group connections to list.
            LobbyGroup group = new LobbyGroup();
            group.groupName = groupName;
            groups.Add(group);

            // create signalr group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task DestroyGroup(string groupName)
        {
            // remove group connections from list.
            var groupToDestroy = groups.FirstOrDefault(g => g.groupName == groupName);
            List<string> connectionsToRemove = (List<string>)groupToDestroy.connections.GetAllConnections();

            // remove all patients from group
            foreach (string connection in connectionsToRemove)
            {
                await Groups.RemoveFromGroupAsync(connection, groupName);
            }

            // remove practitioner from group.
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            // remove destroyed group information from groups list.
            groups.Remove(groupToDestroy);
        }

        //added for group functionality
        public async Task JoinGroup(int patientId, string groupName)
        {
            // add connection mapping to groups list.
            var chosenGroup = groups.FirstOrDefault(g => g.groupName == groupName);
            chosenGroup.connections.Add(patientId, groupName);

            // add them to signalr group.
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"There are now {chosenGroup.connections.Count} in the Lobby.");
        }

        public async Task LeaveGroup(int patientId, string groupName)
        {
            // remove connection mapping from groups list.
            var chosenGroup = groups.FirstOrDefault(g => g.groupName == groupName);
            chosenGroup.connections.Remove(patientId, groupName);

            // remove them from signalr group.
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"There are now {chosenGroup.connections.Count} in the Lobby.");
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", "Connection established successfully.");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", "Connection disconnected successfully.");
            await base.OnDisconnectedAsync(ex);
        }

    }
}
