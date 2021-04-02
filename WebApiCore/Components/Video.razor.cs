using WebApiCore.Interop;
using WebApiCore.Models;
using WebApiCore.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebApiCore.Components
{
    public partial class Video
    {
        [Inject]
        protected IJSRuntime? JavaScript { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        protected ComponentHttpClient _client { get; set; }

        List<RoomDetails> _rooms = new List<RoomDetails>();

        string? _roomName;
        string? _activeCamera;
        string? _activeRoom;
        HubConnection? _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            _client = new ComponentHttpClient();
            _rooms = await _client.GetFromJsonAsync<List<RoomDetails>>("api/twilio/rooms");

            _hubConnection = new HubConnectionBuilder()
                .AddMessagePackProtocol()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubEndpoints.NotificationHub))
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>(HubEndpoints.RoomsUpdated, OnRoomAdded);

            await _hubConnection.StartAsync();
        }

        async ValueTask OnLeaveRoom()
        {
            await JavaScript.LeaveRoomAsync();
            await _hubConnection.InvokeAsync(HubEndpoints.RoomsUpdated, _activeRoom = null);
            if (!string.IsNullOrWhiteSpace(_activeCamera))
            {
                await JavaScript.StartVideoAsync(_activeCamera, "#camera");
            }
        }

        async Task OnCameraChanged(string activeCamera) =>
            await InvokeAsync(() => _activeCamera = activeCamera);

        async Task OnRoomAdded(string roomName) =>
            await InvokeAsync(async () =>
            {
                _rooms = await _client.GetFromJsonAsync<List<RoomDetails>>("api/twilio/rooms");
                StateHasChanged();
            });

        protected async ValueTask TryAddRoom(object args)
        {
            if (_roomName is null || _roomName is { Length: 0 })
            {
                return;
            }

            var takeAction = args switch
            {
                KeyboardEventArgs keyboard when keyboard.Key == "Enter" => true,
                MouseEventArgs _ => true,
                _ => false
            };

            if (takeAction)
            {
                var addedOrJoined = await TryJoinRoom(_roomName);
                if (addedOrJoined)
                {
                    _roomName = null;
                }
            }
        }

        protected async ValueTask<bool> TryJoinRoom(string? roomName)
        {
            if (roomName is null || roomName is { Length: 0 })
            {
                return false;
            }

            var jwt = await _client.GetFromJsonAsync<TwilioJwt>("api/twilio/token");
            if (jwt?.Token is null)
            {
                return false;
            }

            var isRoomInList = _rooms.Any(r => r.Name == roomName);
            if (isRoomInList)
            {
                var roomCreated = await _client.GetAsync("api/twilio/createroom");
            }

            var joined = await JavaScript.CreateOrJoinRoomAsync(roomName, jwt.Token);
            if (joined)
            {
                _activeRoom = roomName;
                await _hubConnection.InvokeAsync(HubEndpoints.RoomsUpdated, _activeRoom);
            }

            return joined;
        }
    }
}
