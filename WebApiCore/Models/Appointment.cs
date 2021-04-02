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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss tt}")]
        [DateValidation(ErrorMessage = "Is this a valid date?")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm: tt}")]
        [DateValidation(ErrorMessage = "Is this a valid date?")]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Appointment Reason")]
        public string AppointmentReason { get; set; }
        [Display(Name = "Appointment Instructions")]
        public string AppointmentInstructions { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string PractitionerID { get; set; }
        public string PatientID { get; set; }

        public string EpicId { get; set; }
        //virtual keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class
        public virtual Patient Patient { get; set; }

        public virtual Practitioner Practitioner { get; set; }
    }
}
