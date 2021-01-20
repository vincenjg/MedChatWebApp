using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string EpicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TestPassword { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

    }
}
