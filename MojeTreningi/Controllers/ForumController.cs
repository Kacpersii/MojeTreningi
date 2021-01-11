using MojeTreningi.DAL;
using MojeTreningi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MojeTreningi.Controllers
{
    public class ForumController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();

        // GET: Forum
        public ActionResult Index()
        {
            var kategorie = db.Kategorie.ToList();
            return View(kategorie);
        }

        public ActionResult Tematy(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ID = id;
            var tematy = db.Tematy.Where(t => t.KategoriaID == id).ToList();
            return View(tematy);
        }

        public ActionResult Temat(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var komentarze = db.Komentarze.Where(k => k.TematID == id).ToList();
            return View(komentarze);
        }

        [Authorize]
        public ActionResult DodajTemat(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.KategoriaID = id;
            ViewBag.KategoriaNazwa = db.Kategorie.Single(k => k.ID == id).Nazwa;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajTemat(/*[Bind(Include = "Temat.KategoriaID,Temat.Nazwa,Komentarz.Tresc")]*/ DodajTematViewModel dodajTematViewModel)
        {
            if (ModelState.IsValid)
            {
                var userID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID;
                dodajTematViewModel.Temat.ProfilID = userID;

                dodajTematViewModel.Komentarz.ProfilID = userID;
                dodajTematViewModel.Komentarz.TematID = dodajTematViewModel.Temat.ID;
                dodajTematViewModel.Komentarz.DataDodania = DateTime.Now;

                db.Tematy.Add(dodajTematViewModel.Temat);
                db.Komentarze.Add(dodajTematViewModel.Komentarz);
                db.SaveChanges();
                return RedirectToAction("Temat", new { id = dodajTematViewModel.Temat.ID });
            }

            ViewBag.KategoriaID = dodajTematViewModel.Temat.KategoriaID;
            return View(dodajTematViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DodajKategorie()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajKategorie([Bind(Include = "Nazwa")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {

                db.Kategorie.Add(kategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategoria);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajKomentarz(int tematID, string tresc)
        {
            Komentarz komentarz = new Komentarz()
            {
                ProfilID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID,
                TematID = db.Tematy.Single(t => t.ID == tematID).ID,
                Tresc = tresc,
                DataDodania = DateTime.Now
            };
            db.Komentarze.Add(komentarz);
            db.SaveChanges();
            return RedirectToAction("Temat", new { id = komentarz.TematID });
        }

        [Authorize]
        [HttpPost, ActionName("Temat")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentarz komentarz = db.Komentarze.Find(id);
            db.Komentarze.Remove(komentarz);
            db.SaveChanges();

            int idTematu = komentarz.TematID;
            return RedirectToAction("Temat", idTematu);
        }
    }
}