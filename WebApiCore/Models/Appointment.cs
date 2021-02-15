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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MMM-dd HH:mm:ss.fff}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0yyyy-MMM-dd HH:mm:ss.fff}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentInstructions { get; set; }
        public int PatientID { get; set; }
        public int PractitionerID { get; set; }
    }
}
