using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCore.Options
{
    public class HubEndpoints
    {
        public const string LobbyHub = "/chathub";
        public const string NotificationHub = "/notifications";
        public const string RoomsUpdated = nameof(RoomsUpdated);
    }
}
