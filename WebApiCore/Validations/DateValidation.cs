using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Validations
{
    public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = (DateTime)value;
            DateTime today = DateTime.Now;
            if (dt.Date.Date.CompareTo(today.Date) >= 0)
                return true;
            else
                return false;
        }
    }
}
