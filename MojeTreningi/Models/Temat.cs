using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Temat
    {
        public int ID { get; set; }
        public int KategoriaID { get; set; }
        public int ProfilID { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Nazwa { get; set; }

        public virtual Kategoria Kategoria { get; set; }
        public virtual Profil Profil { get; set; }
    }
}