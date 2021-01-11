using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MojeTreningi.DAL;
using MojeTreningi.Models;

namespace MojeTreningi.Controllers
{
    public class PlanTreningowyController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();

        // GET: PlanTreningowy
        public ActionResult Index()
        {
            bool isManager = User.IsInRole("Manager");

            PlanyTreningoweViewModel planyTreningoweViewModel = new PlanyTreningoweViewModel
            {
                Prywatne = db.PlanyTreningowe.Where(p => p.Profil.UserName == User.Identity.Name).ToList(),
                Publiczne = db.PlanyTreningowe.Where(p => !p.CzyPrywatny).ToList()
            };
            return View(planyTreningoweViewModel);
        }

        // GET: PlanTreningowy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            PlanTreningowy planTreningowy = db.PlanyTreningowe.Find(id);
            if (planTreningowy == null)
            {
                return HttpNotFound();
            }
            return View(planTreningowy);
        }


        [Authorize]
        public ActionResult PlanTreningowy(int? id)
        {
            if(id == null)
            {
                DodajPlanTreningowyViewModel dodajPlanTreningowyViewModel1 = new DodajPlanTreningowyViewModel
                {
                    Krok = 1,
                };
                ViewBag.ProfilID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID;
                return View(dodajPlanTreningowyViewModel1);
            }
            DodajPlanTreningowyViewModel dodajPlanTreningowyViewModel = new DodajPlanTreningowyViewModel
            {
                Krok = 3,
                PlanTreningowyID = (int)id,
                PlanTreningowy = db.PlanyTreningowe.Single(p => p.ID == (int)id),

            };

            var cwiczenia = db.Cwiczenia.OrderBy(c => c.PartiaCiala.Nazwa);
            ViewBag.CwiczenieID = new SelectList(cwiczenia, "ID", "Nazwa");
            ViewBag.ZestawID = new SelectList(dodajPlanTreningowyViewModel.PlanTreningowy.Zestawy.OrderBy(zes => zes.ID), "ID", "Nazwa", dodajPlanTreningowyViewModel.ZestawID);

            return View(dodajPlanTreningowyViewModel);
        }

        // POST: PlanTreningowy/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlanTreningowy(DodajPlanTreningowyViewModel dodajPlanTreningowyViewModel)
        {
            PlanTreningowy planTreningowy = new PlanTreningowy()
            {
                ProfilID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID,
                Nazwa = dodajPlanTreningowyViewModel.NazwaPlanu,
                CzyPrywatny = dodajPlanTreningowyViewModel.CzyPrywatny
            };
            db.PlanyTreningowe.Add(planTreningowy);
            db.SaveChanges();

            dodajPlanTreningowyViewModel.PlanTreningowy = planTreningowy;
            dodajPlanTreningowyViewModel.PlanTreningowyID = planTreningowy.ID;
            dodajPlanTreningowyViewModel.Krok++;
            return View(dodajPlanTreningowyViewModel);
        }

        // POST: PlanTreningowy/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateZestaw(DodajPlanTreningowyViewModel dodajPlanTreningowyViewModel)
        {
            Zestaw zestaw = new Zestaw
            {
                PlanTreningowyID = dodajPlanTreningowyViewModel.PlanTreningowyID,
                Nazwa = dodajPlanTreningowyViewModel.NazwaZestawu
            };
            db.Zestawy.Add(zestaw);
            db.SaveChanges();

            dodajPlanTreningowyViewModel.NazwaZestawu = "";
            dodajPlanTreningowyViewModel.PlanTreningowy = db.PlanyTreningowe.Single(p => p.ID == dodajPlanTreningowyViewModel.PlanTreningowyID);
            dodajPlanTreningowyViewModel.Krok++;

            var z = db.Zestawy.Single(ze => ze.ID == zestaw.ID);

            var cwiczenia = db.Cwiczenia.OrderBy(c => c.Nazwa);
            ViewBag.CwiczenieID = new SelectList(cwiczenia, "ID", "Nazwa", dodajPlanTreningowyViewModel.CwiczenieID);
            ViewBag.ZestawID = new SelectList(dodajPlanTreningowyViewModel.PlanTreningowy.Zestawy.OrderBy(zes => zes.ID), "ID", "Nazwa", dodajPlanTreningowyViewModel.ZestawID);
            return View("PlanTreningowy", dodajPlanTreningowyViewModel);
        }

        // POST: PlanTreningowy/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCwiczenie(DodajPlanTreningowyViewModel dodajPlanTreningowyViewModel)
        {
            CwiczenieWPlanie cwiczenie = new CwiczenieWPlanie
            {
                CwiczenieID = dodajPlanTreningowyViewModel.CwiczenieID,
                Cwiczenie = db.Cwiczenia.Single(c => c.ID == dodajPlanTreningowyViewModel.CwiczenieID),
                ZestawID = dodajPlanTreningowyViewModel.ZestawID,
                Serie = dodajPlanTreningowyViewModel.Serie,
                Powtorzenia = dodajPlanTreningowyViewModel.Powtorzenia,
                PrzerwaPomiedzySeriamiMinuty = dodajPlanTreningowyViewModel.PrzerwaPomiedzySeriamiMinuty,
                PrzerwaPomiedzySeriamiSekundy = dodajPlanTreningowyViewModel.PrzerwaPomiedzySeriamiSekundy,
                PrzerwaPoCwiczeniuMinuty = dodajPlanTreningowyViewModel.PrzerwaPoCwiczeniuMinuty,
                PrzerwaPoCwiczeniuSekundy = dodajPlanTreningowyViewModel.PrzerwaPoCwiczeniuSekundy,
            };
            db.CwiczeniaWPlanie.Add(cwiczenie);
            db.SaveChanges();

            dodajPlanTreningowyViewModel.PlanTreningowy = db.PlanyTreningowe.Single(p => p.ID == dodajPlanTreningowyViewModel.PlanTreningowyID);

            var cwiczenia = db.Cwiczenia.OrderBy(c => c.PartiaCiala.Nazwa);
            ViewBag.CwiczenieID = new SelectList(cwiczenia, "ID", "Nazwa");
            ViewBag.ZestawID = new SelectList(dodajPlanTreningowyViewModel.PlanTreningowy.Zestawy.OrderBy(zes => zes.ID), "ID", "Nazwa", dodajPlanTreningowyViewModel.ZestawID);
            return View("PlanTreningowy", dodajPlanTreningowyViewModel);
        }
        



        // - - - E D I T - - - 



        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanTreningowy planTreningowy = db.PlanyTreningowe.Find(id);
            if (planTreningowy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfilID = new SelectList(db.Profile, "ID", "UserName", planTreningowy.ProfilID);
            return View(planTreningowy);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanTreningowy planTreningowy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planTreningowy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PlanTreningowy", new { id = planTreningowy.ID });
            }
            ViewBag.ProfilID = new SelectList(db.Profile, "ID", "UserName", planTreningowy.ProfilID);
            return View(planTreningowy);
        }
        [Authorize]
        public ActionResult EditZestaw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zestaw zestaw = db.Zestawy.Find(id);
            if (zestaw == null)
            {
                return HttpNotFound();
            }
            return View(zestaw);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditZestaw(Zestaw zestaw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zestaw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PlanTreningowy", new { id = zestaw.PlanTreningowyID });
            }
            return View(zestaw);
        }
        [Authorize]
        public ActionResult EditCwiczenie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CwiczenieWPlanie cwiczenieWPlanie = db.CwiczeniaWPlanie.Find(id);
            if (cwiczenieWPlanie == null)
            {
                return HttpNotFound();
            }
            var cwiczenia = db.Cwiczenia.OrderBy(c => c.PartiaCiala.Nazwa);
            ViewBag.CwiczenieID = new SelectList(cwiczenia, "ID", "Nazwa", cwiczenieWPlanie.CwiczenieID);

            var planID = db.Zestawy.Single(z => z.ID == cwiczenieWPlanie.ZestawID).PlanTreningowyID;
            ViewBag.ZestawID = new SelectList(db.Zestawy.Where(z => z.PlanTreningowyID == planID), "ID", "Nazwa", cwiczenieWPlanie.ZestawID);
            return View(cwiczenieWPlanie);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCwiczenie(CwiczenieWPlanie cwiczenieWPlanie)
        {
            var planID = db.Zestawy.Single(z => z.ID == cwiczenieWPlanie.ZestawID).PlanTreningowyID;
            if (ModelState.IsValid)
            {
                db.Entry(cwiczenieWPlanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PlanTreningowy", new { id = planID });
            }
            var cwiczenia = db.Cwiczenia.OrderBy(c => c.PartiaCiala.Nazwa);
            ViewBag.CwiczenieID = new SelectList(cwiczenia, "ID", "Nazwa", cwiczenieWPlanie.CwiczenieID);
            ViewBag.ZestawID = new SelectList(db.Zestawy.Where(z => z.PlanTreningowyID == planID), "ID", "Nazwa", cwiczenieWPlanie.ZestawID);
            return View(cwiczenieWPlanie);
        }



        // - - - D E L E T E - - - 



        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanTreningowy planTreningowy = db.PlanyTreningowe.Find(id);
            if (planTreningowy == null)
            {
                return HttpNotFound();
            }
            return View(planTreningowy);
        }

        // POST: PlanTreningowy/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanTreningowy planTreningowy = db.PlanyTreningowe.Find(id);
            db.PlanyTreningowe.Remove(planTreningowy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult DeleteZestaw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zestaw zestaw = db.Zestawy.Find(id);
            if (zestaw == null)
            {
                return HttpNotFound();
            }
            return View(zestaw);
        }

        [Authorize]
        [HttpPost, ActionName("DeleteZestaw")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedZestaw(int id)
        {
            Zestaw zestaw = db.Zestawy.Find(id);
            db.Zestawy.Remove(zestaw);
            db.SaveChanges();
            return RedirectToAction("PlanTreningowy", new { id = zestaw.PlanTreningowyID });
        }
        [Authorize]
        public ActionResult DeleteCwiczenie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CwiczenieWPlanie cwiczenie = db.CwiczeniaWPlanie.Find(id);
            if (cwiczenie == null)
            {
                return HttpNotFound();
            }
            return View(cwiczenie);
        }

        [Authorize]
        [HttpPost, ActionName("DeleteCwiczenie")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedCwiczenie(int id)
        {
            CwiczenieWPlanie cwiczenie = db.CwiczeniaWPlanie.Find(id);
            db.CwiczeniaWPlanie.Remove(cwiczenie);
            db.SaveChanges();
            int planID = db.Zestawy.Single(z => z.ID == cwiczenie.ZestawID).PlanTreningowyID;
            return RedirectToAction("PlanTreningowy", new { id = planID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
