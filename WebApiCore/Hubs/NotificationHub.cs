using WebApiCore.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApiCore.Hubs
{
    public class NotificationHub : Hub
    {
        public Task RoomsUpdated(string room) =>
            Clients.All.SendAsync(HubEndpoints.RoomsUpdated, room);
    }
}
