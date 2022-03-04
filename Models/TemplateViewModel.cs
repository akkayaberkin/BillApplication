using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillApplication.Models
{
    public class TemplateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TemplatePath { get; set; }
        public int IsActive { get; set; }
    }
}