using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Practitioner
    {
        public int PractitionerID { get; set; }

        [Required(ErrorMessage = "Please fill out the field")]
        [Display(Name = "Doctor's Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please fill out the field")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please fill out the field")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill out the field")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please fill out the field")]
        public string TestPassword { get; set; }
        [Required(ErrorMessage = "Please fill out the field")]
        public bool IsOnline { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName + " " + Title;
        }
    }
}
