using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class Pomiar
    {
        public int ID { get; set; }
        public int ProfilID { get; set; }
        [Display(Name = "Data pomiaru")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPomiaru { get; set; }
        public double Waga { get; set; }
        public double Wzrost { get; set; }
        public double Kark { get; set; }
        [Display(Name = "Klatka piersiowa")]
        public double KlatkaPiersiowa { get; set; }
        public double Talia { get; set; }
        public double Pas { get; set; }
        public double Biodro { get; set; }
        public double Ramie { get; set; }
        public double Przedramie { get; set; }
        public double Udo { get; set; }
        public double Łydka { get; set; }
        public string Zdjecie { get; set; }

        public virtual Profil Profil { get; set; }
    }
}