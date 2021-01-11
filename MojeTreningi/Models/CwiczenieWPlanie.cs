using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class CwiczenieWPlanie
    {
        public int ID { get; set; }
        public int ZestawID { get; set; }
        public int CwiczenieID { get; set; }
        [Display(Name = "Ilość serii")]
        public int Serie { get; set; }
        [Display(Name = "Ilość powtórzeń")]
        public int Powtorzenia { get; set; }
        public int PrzerwaPomiedzySeriamiMinuty { get; set; }
        public int PrzerwaPomiedzySeriamiSekundy { get; set; }
        public int PrzerwaPoCwiczeniuMinuty { get; set; }
        public int PrzerwaPoCwiczeniuSekundy { get; set; }

        public virtual Zestaw Zestaw { get; set; }
        public virtual Cwiczenie Cwiczenie { get; set; }
    }
}