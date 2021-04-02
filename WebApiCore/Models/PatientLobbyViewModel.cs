using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class PatientLobbyViewModel
    {
        public List<Practitioner>? Practitioners { get; set; }

        [BindProperty]
        public string? SelectedPractitioner { get; set; }

        public string? LobbyMessage { get; set; }
    }
}
