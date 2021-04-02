using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Lobby
    {
        public string LobbyName { get; set; } = null!;
        public Practitioner Practitioner { get; set; } = null!;
        public List<Patient> Patients { get; set; } = null!;
    }
}
