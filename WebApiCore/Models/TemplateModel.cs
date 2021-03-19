using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{

    public class TemplateModel
    {

        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateData { get; set; }
        public int PractitionerID { get; set; }
        [NotMapped]
        public SelectList DropDownList { get; set; }

        //public List<TemplateModel> DropDownList { get; set; }
    }
}
