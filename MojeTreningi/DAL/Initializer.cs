using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MojeTreningi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MojeTreningi.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<MojeTreningiContext>
    {
        protected override void Seed(MojeTreningiContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));


            //  UZYTKOWNICY

            var user0 = new ApplicationUser { UserName = "admin@admin.com" };
            string password0 = "Admin.123";
            userManager.Create(user0, password0);
            userManager.AddToRole(user0.Id, "ADMIN");

            var user1 = new ApplicationUser { UserName = "qwer@qwer.com" };
            string password1 = "Qwer.123";
            userManager.Create(user1, password1);
            userManager.AddToRole(user1.Id, "User");

            var user2 = new ApplicationUser { UserName = "asdf@asdf.com" };
            string password2 = "Asdf.123";
            userManager.Create(user2, password2);
            userManager.AddToRole(user2.Id, "User");

            var user3 = new ApplicationUser { UserName = "zxcv@zxcv.com" };
            string password3 = "Zxcv.123";
            userManager.Create(user3, password3);
            userManager.AddToRole(user3.Id, "User");

            var profile = new List<Profil>
            {
                new Profil { UserName = user0.UserName, Imie = "ADMIN", Nazwisko = "ADMIN", DataUrodzenia = DateTime.Parse("1994-01-28") },
                new Profil { UserName = user1.UserName, Imie = "Artur", Nazwisko = "Komar", DataUrodzenia = DateTime.Parse("1994-01-28") },
                new Profil { UserName = user2.UserName, Imie = "Filip", Nazwisko = "Rasiak", DataUrodzenia = DateTime.Parse("1997-08-13")},
                new Profil { UserName = user3.UserName, Imie = "Karol", Nazwisko = "Górski", DataUrodzenia = DateTime.Parse("2003-05-25")}
            };
            profile.ForEach(u => context.Profile.Add(u));
            context.SaveChanges();

            var partieCiala = new List<PartiaCiala>
            {
                new PartiaCiala { Nazwa = "Klatka piersiowa"},
                new PartiaCiala { Nazwa = "Plecy"},
                new PartiaCiala { Nazwa = "Barki"},
                new PartiaCiala { Nazwa = "Triceps"},
                new PartiaCiala { Nazwa = "Biceps"},
                new PartiaCiala { Nazwa = "Przedramie"},
                new PartiaCiala { Nazwa = "Brzuch"},
                new PartiaCiala { Nazwa = "Pośladki"},
                new PartiaCiala { Nazwa = "Czworogłowy uda"},
                new PartiaCiala { Nazwa = "Dwugłowy uda"},
                new PartiaCiala { Nazwa = "Łydki"},
            };
            partieCiala.ForEach(p => context.PartieCiala.Add(p));
            context.SaveChanges();

            //  POMIARY

            var pomiary = new List<Pomiar>
            {
                new Pomiar
                {
                    Profil = profile[1],
                    DataPomiaru = DateTime.Parse("2020-10-07"),
                    Waga = 83,
                    Wzrost = 178,
                    Kark = 40,
                    KlatkaPiersiowa = 97,
                    Talia = 84,
                    Pas = 88,
                    Biodro = 98,
                    Ramie = 34,
                    Przedramie = 28,
                    Udo = 59,
                    Łydka = 39,
                    Zdjecie = "qwe.jpg"
                },
                new Pomiar
                {
                    Profil = profile[2],
                    DataPomiaru = DateTime.Parse("2020-10-07"),
                    Waga = 91,
                    Wzrost = 183,
                    Kark = 44,
                    KlatkaPiersiowa = 100,
                    Talia = 88,
                    Pas = 88,
                    Biodro = 95,
                    Ramie = 33.5,
                    Przedramie = 28,
                    Udo = 62,
                    Łydka = 40
                },
                new Pomiar
                {
                    Profil = profile[3],
                    DataPomiaru = DateTime.Parse("2020-10-10"),
                    Waga = 67,
                    Wzrost = 180,
                    Kark = 36.5,
                    KlatkaPiersiowa = 92,
                    Talia = 76.5,
                    Pas = 80.5,
                    Biodro = 93.5,
                    Ramie = 30,
                    Przedramie = 26,
                    Udo = 53,
                    Łydka = 36.5,
                    Zdjecie = "asd.jpg"
                }
            };
            pomiary.ForEach(p => context.Pomiary.Add(p));
            context.SaveChanges();






            //  CWICZENIA

            var cwiczenia = new List<Cwiczenie>
            {
                new Cwiczenie { PartiaCiala = partieCiala[0], Nazwa = "Wyciskanie sztangi na ławce leżąć", Opis = "qwe"},
                new Cwiczenie { PartiaCiala = partieCiala[0], Nazwa = "Wyciskanie hantli na ławce leżąć", Opis = ""},
                new Cwiczenie { PartiaCiala = partieCiala[0], Nazwa = "Pompki", Opis = "qq"},
                new Cwiczenie { PartiaCiala = partieCiala[0], Nazwa = "Rozpiętki hantlami leżąc", Opis = "dd"},
                new Cwiczenie { PartiaCiala = partieCiala[1], Nazwa = "Podciąganie na drążku", Opis = "bb"},
                new Cwiczenie { PartiaCiala = partieCiala[1], Nazwa = "Wiosłowanie sztangą w opadzie tułowia", Opis = "qq"},
                new Cwiczenie { PartiaCiala = partieCiala[1], Nazwa = "Ściąganie drążka wyciągu górnego do klatki piersiowej", Opis = "ss"},
                new Cwiczenie { PartiaCiala = partieCiala[2], Nazwa = "Wyciskanie żołnierskie", Opis = "dd"},
                new Cwiczenie { PartiaCiala = partieCiala[2], Nazwa = "Wyciskanie hantli nad głowę siedząć", Opis = "vv"},
                new Cwiczenie { PartiaCiala = partieCiala[2], Nazwa = "Unoszenie hantli na bok siedząć", Opis = "zz"},
                new Cwiczenie { PartiaCiala = partieCiala[2], Nazwa = "Unoszenie hantli na bok siedząć w opadzie tułowia", Opis = "ss"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Wyciskanie francuskie sztangi leżąc", Opis = "dd"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Wyciskanie francuskie sztangi stojąc", Opis = "ff"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Wyciskanie francuskie hantli leżąc", Opis = "ttt"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Wyciskanie francuskie hantli stojąc", Opis = "rrr"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Wyciskanie sztangi w wąskim chwycie leżąc", Opis = "eee"},
                new Cwiczenie { PartiaCiala = partieCiala[3], Nazwa = "Prostowanie przedramion z linkami wyciągu górnego", Opis = "www"},
                new Cwiczenie { PartiaCiala = partieCiala[4], Nazwa = "Uginanie przedramion ze sztangą stojąc", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[4], Nazwa = "Uginanie przedramion z hantlami siedząc", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[4], Nazwa = "Uginanie przedramion z hantlami w oparciu o udo siedząc", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[4], Nazwa = "Uginanie przedramion z hantlami z rotacją nadgarstka", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[4], Nazwa = "Uginanie przedramion z hantlami chwytem młotkowym", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[6], Nazwa = "Brzuszki", Opis = "hh"},
                new Cwiczenie { PartiaCiala = partieCiala[6], Nazwa = "Deska", Opis = "hh"},
                new Cwiczenie { PartiaCiala = partieCiala[6], Nazwa = "Allahy", Opis = "v"},
                new Cwiczenie { PartiaCiala = partieCiala[6], Nazwa = "Przyciąganie nóg do klatki piersiowej w zwisie na drążku", Opis = "qqqqq"},
                new Cwiczenie { PartiaCiala = partieCiala[8], Nazwa = "Przysiady", Opis = "r"},
                new Cwiczenie { PartiaCiala = partieCiala[8], Nazwa = "Przysiady ze sztangą", Opis = "r"},
                new Cwiczenie { PartiaCiala = partieCiala[8], Nazwa = "Przysiady z hantlami", Opis = "e"},
                new Cwiczenie { PartiaCiala = partieCiala[8], Nazwa = "Przysiady bułgarskie z hantlami", Opis = "w"},
                new Cwiczenie { PartiaCiala = partieCiala[9], Nazwa = "Martwy ciąg ze sztangą", Opis = "q"},
                new Cwiczenie { PartiaCiala = partieCiala[9], Nazwa = "Martwy ciąg sumo ze sztangą", Opis = "q"},
                new Cwiczenie { PartiaCiala = partieCiala[9], Nazwa = "Martwy ciąg na prostych ze sztangą", Opis = "q"},
                new Cwiczenie { PartiaCiala = partieCiala[9], Nazwa = "Martwy ciąg z hantlami", Opis = "q"},
                new Cwiczenie { PartiaCiala = partieCiala[9], Nazwa = "Wykroki", Opis = "qasd"},
                new Cwiczenie { PartiaCiala = partieCiala[10], Nazwa = "Wspięcia na palce na podwyższeniu", Opis = "qasd"},
            };
            cwiczenia.ForEach(c => context.Cwiczenia.Add(c));
            context.SaveChanges();

            //  PLAN TRENINGOWY

            var planyTreningowe = new List<PlanTreningowy>
            {
                new PlanTreningowy { Profil = profile[0], Nazwa = "Trening dla każdego w domu bez sprzętu", CzyPrywatny = false },
                new PlanTreningowy { Profil = profile[0], Nazwa = "Trening FBW dla początkujących", CzyPrywatny = false },
                new PlanTreningowy { Profil = profile[1], Nazwa = "Pierwszy trening", CzyPrywatny = true },
            };
            planyTreningowe.ForEach(p => context.PlanyTreningowe.Add(p));
            context.SaveChanges();

            var zestawy = new List<Zestaw>
            {
                new Zestaw { PlanTreningowy = planyTreningowe[0], Nazwa = "A" },
                new Zestaw { PlanTreningowy = planyTreningowe[1], Nazwa = "A" },
                new Zestaw { PlanTreningowy = planyTreningowe[1], Nazwa = "B" },
                new Zestaw { PlanTreningowy = planyTreningowe[2], Nazwa = "Poniedziałek" },
                new Zestaw { PlanTreningowy = planyTreningowe[2], Nazwa = "Środa" },
                new Zestaw { PlanTreningowy = planyTreningowe[2], Nazwa = "Piątek" },
            };
            zestawy.ForEach(z => context.Zestawy.Add(z));
            context.SaveChanges();

            var cwiczeniaWPlanie = new List<CwiczenieWPlanie>
            {
                new CwiczenieWPlanie { Zestaw = zestawy[0], Cwiczenie = cwiczenia[26], Serie = 4, Powtorzenia = 15, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 0, PrzerwaPoCwiczeniuMinuty = 1, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[0], Cwiczenie = cwiczenia[2], Serie = 4, Powtorzenia = 10, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 0, PrzerwaPoCwiczeniuMinuty = 1, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[0], Cwiczenie = cwiczenia[34], Serie = 3, Powtorzenia = 10, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 0, PrzerwaPoCwiczeniuMinuty = 1, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[0], Cwiczenie = cwiczenia[22], Serie = 3, Powtorzenia = 20, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 0, PrzerwaPoCwiczeniuMinuty = 1, PrzerwaPoCwiczeniuSekundy = 0 },
                
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[27], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[0], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[5], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[9], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[11], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[17], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[23], Serie = 3, Powtorzenia = 30, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[1], Cwiczenie = cwiczenia[35], Serie = 3, Powtorzenia = 10, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[30], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[7], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[4], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[15], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[19], Serie = 5, Powtorzenia = 5, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[24], Serie = 3, Powtorzenia = 15, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[2], Cwiczenie = cwiczenia[35], Serie = 3, Powtorzenia = 10, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },

                new CwiczenieWPlanie { Zestaw = zestawy[3], Cwiczenie = cwiczenia[27], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[3], Cwiczenie = cwiczenia[30], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[3], Cwiczenie = cwiczenia[35], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                
                new CwiczenieWPlanie { Zestaw = zestawy[4], Cwiczenie = cwiczenia[0], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[4], Cwiczenie = cwiczenia[7], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[4], Cwiczenie = cwiczenia[12], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[4], Cwiczenie = cwiczenia[23], Serie = 3, Powtorzenia = 30, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                
                new CwiczenieWPlanie { Zestaw = zestawy[5], Cwiczenie = cwiczenia[5], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[5], Cwiczenie = cwiczenia[20], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[5], Cwiczenie = cwiczenia[10], Serie = 3, Powtorzenia = 12, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
                new CwiczenieWPlanie { Zestaw = zestawy[5], Cwiczenie = cwiczenia[24], Serie = 3, Powtorzenia = 15, PrzerwaPomiedzySeriamiMinuty = 1, PrzerwaPomiedzySeriamiSekundy = 30, PrzerwaPoCwiczeniuMinuty = 2, PrzerwaPoCwiczeniuSekundy = 0 },
            };
            cwiczeniaWPlanie.ForEach(c => context.CwiczeniaWPlanie.Add(c));
            context.SaveChanges();

            //  FORUM

            var kategorie = new List<Kategoria>
            {
                new Kategoria { Nazwa = "Dieta"},
                new Kategoria { Nazwa = "Ćwiczenia"},
                new Kategoria { Nazwa = "Siłownie"},
                new Kategoria { Nazwa = "Kontuzje"},

            };
            kategorie.ForEach(k => context.Kategorie.Add(k));
            context.SaveChanges();

            var tematy = new List<Temat>
            {
                new Temat { Nazwa = "Białko po treningu ", Profil = profile[3], Kategoria = kategorie[0]},
                new Temat { Nazwa = "Najlepsze produkty wege", Profil = profile[3], Kategoria = kategorie[0]},
                new Temat { Nazwa = "Według naukowców to ćwiczenie wystarczy do zrobienia 6paku", Profil = profile[1], Kategoria = kategorie[1]},
                new Temat { Nazwa = "Ściąganie drążka wyciągu do karku", Profil = profile[3], Kategoria = kategorie[3]},
                new Temat { Nazwa = "Ból w barku podczas wyciskania", Profil = profile[3], Kategoria = kategorie[3]}
                
            };
            tematy.ForEach(t => context.Tematy.Add(t));
            context.SaveChanges();

            var komentarze = new List<Komentarz>
            {
                new Komentarz { Temat = tematy[0], Profil = profile[3], Tresc = "trzeba jeść od razu białko po treningu czy można poczekać?", DataDodania = DateTime.Parse("2020-10-07 13:11")},
                new Komentarz { Temat = tematy[0], Profil = profile[2], Tresc = "nie musisz od razu", DataDodania = DateTime.Parse("2020-10-07 13:33")},
                new Komentarz { Temat = tematy[1], Profil = profile[3], Tresc = "Jakie są najlepsze produkty do budowania masy dla wegan?", DataDodania = DateTime.Parse("2020-10-07 13:55")},
                new Komentarz { Temat = tematy[2], Profil = profile[1], Tresc = "Naukowcy zbadali, że unoszenie kolan w zwisie na drążku jest o 248% efektywnijesze od zwykłych brzuszków", DataDodania = DateTime.Parse("2020-10-07 14:19")},
                new Komentarz { Temat = tematy[2], Profil = profile[2], Tresc = "Jaka ściema", DataDodania = DateTime.Parse("2020-10-07 14:57")},
                new Komentarz { Temat = tematy[3], Profil = profile[3], Tresc = "Czy ściąganie drążka wyciągu do karku jest zdrowe Bo czytałem ostatnio że nie?", DataDodania = DateTime.Parse("2020-10-07 15:04")},
                new Komentarz { Temat = tematy[4], Profil = profile[3], Tresc = "Mam ból w barku podczas wyciskania sztangi na ławeczce. Jaka może być przyczyna?", DataDodania = DateTime.Parse("2020-10-07 17:40")},
            };
            komentarze.ForEach(k => context.Komentarze.Add(k));
            context.SaveChanges();

        }
    }
}