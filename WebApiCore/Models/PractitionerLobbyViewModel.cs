using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class PractitionerLobbyViewModel
    {
        [BindProperty]
        public bool IsOnline { get; set; }
        public string LobbyName { get; set; }
        public List<Patient>? Patients { get; set; }

    }
}
