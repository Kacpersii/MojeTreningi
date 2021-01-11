using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class DodajPlanTreningowyViewModel
    {
        public int Krok { get; set; }
        public int PlanTreningowyID { get; set; }
        public string NazwaPlanu { get; set; }
        public bool CzyPrywatny { get; set; }
        public int ZestawID { get; set; }
        public string NazwaZestawu { get; set; }
        public int CwiczenieID { get; set; }
        public int Serie { get; set; }
        public int Powtorzenia { get; set; }
        public int PrzerwaPomiedzySeriamiMinuty { get; set; }
        public int PrzerwaPomiedzySeriamiSekundy { get; set; }
        public int PrzerwaPoCwiczeniuMinuty { get; set; }
        public int PrzerwaPoCwiczeniuSekundy { get; set; }

        public virtual PlanTreningowy PlanTreningowy { get; set; }
        public virtual List<Zestaw> Zestawy { get; set; }
    }
}