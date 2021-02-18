using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Validations;

namespace WebApiCore.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [DateValidation(ErrorMessage = "Is this a valid date?")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [DateValidation(ErrorMessage = "Is this a valid date?")]
        public DateTime EndTime { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentInstructions { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public int PractitionerID { get; set; }
        public int PatientID { get; set; }
        //virtual keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class
        public virtual Patient Patient { get; set; }

        public virtual Practitioner Practitioner { get; set; }
    }
}
