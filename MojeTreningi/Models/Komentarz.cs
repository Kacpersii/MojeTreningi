using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Komentarz
    {
        public int ID { get; set; }
        public int TematID { get; set; }
        public int ProfilID { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 2)]
        public string Tresc { get; set; }
        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; }

        public virtual Temat Temat { get; set; }
        public virtual Profil Profil { get; set; }
    }
}