using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Profil
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Imie { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Nazwisko { get; set; }
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataUrodzenia { get; set; }

        public virtual List<Temat> Tematy { get; set; }
        public virtual List<Komentarz> Komentarze { get; set; }
        public virtual List<Pomiar> Pomiary { get; set; }
        public virtual List<PlanTreningowy> PlanyTreningowe { get; set; }
    }
}