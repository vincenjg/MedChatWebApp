using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Practitioner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string EmailAddress { get; set; }
        public string TestPassword { get; set; }
        public bool IsOnline { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName + " " + Title;
        }
    }
}
