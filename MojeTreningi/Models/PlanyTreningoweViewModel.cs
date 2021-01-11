using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class PlanyTreningoweViewModel
    {
        public List<PlanTreningowy> Prywatne { get; set; }
        public List<PlanTreningowy> Publiczne { get; set; }
    }
}