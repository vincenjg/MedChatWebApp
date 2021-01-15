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
        public string Password { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName + " " + Title;
        }
    }
}
