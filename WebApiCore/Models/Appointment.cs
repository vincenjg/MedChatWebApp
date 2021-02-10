using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentInstructions { get; set; }
        public int PatientID { get; set; }
        public int PractitionerID { get; set; }
    }
}
