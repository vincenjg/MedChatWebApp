using WebApiCore.Models;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebApiCore.Interop
{
    public static class VideoJS
    {
        public static ValueTask<Device[]> GetVideoDevicesAsync(
            this IJSRuntime? jsRuntime) =>
            jsRuntime?.InvokeAsync<Device[]>(
                "videoInterop.getVideoDevices") ?? new ValueTask<Device[]>();

        public static ValueTask StartVideoAsync(
            this IJSRuntime? jsRuntime,
            string deviceId,
            string selector) =>
            jsRuntime?.InvokeVoidAsync(
                "videoInterop.startVideo",
                deviceId, selector) ?? new ValueTask();

        public static ValueTask<bool> CreateOrJoinRoomAsync(
            this IJSRuntime? jsRuntime,
            string roomName,
            string token) =>
            jsRuntime?.InvokeAsync<bool>(
                "videoInterop.createOrJoinRoom",
                roomName, token) ?? new ValueTask<bool>();

        public static ValueTask LeaveRoomAsync(
            this IJSRuntime? jsRuntime) =>
            jsRuntime?.InvokeVoidAsync(
                "videoInterop.leaveRoom") ?? new ValueTask();
    }
}
