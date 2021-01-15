using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace WebApiCore.Hubs
{
    public class ChatHub : Hub
    {
        
        public void NewMessage(string username, string message)
        {
            Clients.Others.SendAsync(username, message, DateTime.Now);
        }

        //added for group functionality
        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            //the context.connection id will have to be changed to actual user name
            await Clients.Group(group).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has joined the group {group}.");
        }

        public async Task SendMessageToAll(string message)
        {
            //this doesn't send message to everyone. only those tho joined the private group.
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.User.Identity.Name}: {message}");
            //following will allow option to send message to everyone.
            //await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToCaller(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            //await Clients.User(user).SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToGroup(string group, string message)
        {
            //await Clients.Group(group).SendAsync("ReceiveMessage", message);
            await Clients.Group(group).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name}: {message}");
        }

        /*public async Task SendPrivateMessage(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", message);
        }*/

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.User.Identity.Name);
            await base.OnDisconnectedAsync(ex);
        }

    }
}
