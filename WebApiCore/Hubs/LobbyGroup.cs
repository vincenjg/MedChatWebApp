using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Hubs
{
    public class LobbyGroup
    {
        // the unique group name, may have to int because physicians could have the same name.
        public string groupName;

        // holds a pairing of patientID and signalr connectionID, to map connections to patients.
        public readonly ConnectionMapping<int> connections = new ConnectionMapping<int>();
    }
}
