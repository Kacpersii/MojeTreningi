using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Cwiczenie
    {
        public int ID { get; set; }
        public int PartiaCialaID { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Nazwa { get; set; }
        public string Opis { get; set; }

        public virtual PartiaCiala PartiaCiala { get; set; }
    }
}