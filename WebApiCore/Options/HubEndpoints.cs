using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCore.Options
{
    public class HubEndpoints
    {
        public const string NotificationHub = "/notifications";
        public const string RoomsUpdated = nameof(RoomsUpdated);
        public const string LobbyHub = "/lobbyhub";
        public const string ChangeStatus = nameof(ChangeStatus);
        public const string CreatLobby = nameof(CreatLobby);
        public const string JoinLobby = nameof(JoinLobby);
        public const string LeaveLobby = nameof(LeaveLobby);
        public const string DestroyLobby = nameof(DestroyLobby);
    }
}
