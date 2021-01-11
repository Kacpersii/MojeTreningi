using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class PlanTreningowy
    {
        public int ID { get; set; }
        public int ProfilID { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Nazwa { get; set; }
        [DefaultValue(false)]
        public bool CzyPrywatny { get; set; }

        public virtual Profil Profil { get; set; }
        public virtual List<Zestaw> Zestawy { get; set; }
    }
}