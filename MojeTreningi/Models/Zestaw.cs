using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Zestaw
    {
        public int ID { get; set; }
        public int PlanTreningowyID { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Nazwa { get; set; }

        public virtual PlanTreningowy PlanTreningowy { get; set; }
        public virtual List<CwiczenieWPlanie> CwiczeniaWPlanie { get; set; }
    }
}