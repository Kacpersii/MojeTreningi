using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojeTreningi.Models
{
    public class DodajTematViewModel
    {
        public Temat Temat { get; set; }
        public Komentarz Komentarz { get; set; }

        public DodajTematViewModel()
        {
            Temat = new Temat();
            Komentarz = new Komentarz();
        }
    }
}