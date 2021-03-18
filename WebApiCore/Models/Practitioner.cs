using Microsoft.AspNetCore.Identity;
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

        [Display(Name = "Doctor's Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string EmailAddress { get; set; }
        public string TestPassword { get; set; }
        public bool IsOnline { get; set; }
        
        public string FullName
        {
            get => FirstName + " " + LastName + " " + Title;
        }

        //things added to follow Microsoft Identity
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

    }
}
