﻿using WebApiCore.Interop;
using WebApiCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebApiCore.Components
{
    public partial class Cameras
    {
        [Inject]
        protected IJSRuntime? JavaScript { get; set; }

        [Parameter]
        public EventCallback<string> CameraChanged { get; set; }

        protected Device[]? Devices { get; private set; }
        protected CameraState State { get; private set; }
        protected bool HasDevices => State == CameraState.FoundCameras;
        protected bool IsLoading => State == CameraState.LoadingCameras;

        string? _activeCamera;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Devices = await JavaScript.GetVideoDevicesAsync();
            State = Devices != null && Devices.Length > 0
                    ? CameraState.FoundCameras
                    : CameraState.Error;
        }

        protected async ValueTask SelectCamera(string deviceId)
        {
            await JavaScript.StartVideoAsync(deviceId, "#camera");
            _activeCamera = deviceId;

            if (CameraChanged.HasDelegate)
            {
                await CameraChanged.InvokeAsync(_activeCamera);
            }
        }
    }
}
