using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojeTreningi.DAL;
using MojeTreningi.Models;

namespace MojeTreningi.Controllers
{
    [Authorize]
    public class PomiaryController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();

        // GET: Pomiary
        public ActionResult Index()
        {
            var pomiary = db.Pomiary.Where(p => p.Profil.UserName == User.Identity.Name).OrderByDescending(p => p.DataPomiaru).ThenByDescending(p => p.ID);
            //var pomiary = db.Pomiary.Include(p => p.Profil);
            return View(pomiary.ToList());
        }

        // GET: Pomiary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pomiar pomiar = db.Pomiary.Find(id);
            if (pomiar == null)
            {
                return HttpNotFound();
            }
            return View(pomiar);
        }

        // GET: Pomiary/Create
        public ActionResult Create()
        {
            Pomiar pomiar = new Pomiar() { DataPomiaru = DateTime.Now.Date };
            return View(pomiar);
        }

        // POST: Pomiary/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataPomiaru,Waga,Wzrost,Kark,KlatkaPiersiowa,Talia,Pas,Biodro,Ramie,Przedramie,Udo,Łydka")] Pomiar pomiar)
        {
            if (ModelState.IsValid)
            {
                pomiar.ProfilID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID;

                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if(file != null && file.ContentLength > 0)
                {
                    pomiar.Zdjecie = System.Guid.NewGuid().ToString() + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Zdjecia/") + pomiar.Zdjecie);
                }

                db.Pomiary.Add(pomiar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfilID = new SelectList(db.Profile, "ID", "UserName", pomiar.ProfilID);
            return View(pomiar);
        }

        // GET: Pomiary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pomiar pomiar = db.Pomiary.Find(id);
            if (pomiar == null)
            {
                return HttpNotFound();
            }
            return View(pomiar);
        }

        // POST: Pomiary/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DataPomiaru,Waga,Wzrost,Kark,KlatkaPiersiowa,Talia,Pas,Biodro,Ramie,Przedramie,Udo,Łydka,Zdjecie")] Pomiar pomiar)
        {
            if (ModelState.IsValid)
            {
                pomiar.ProfilID = db.Profile.Single(p => p.UserName == User.Identity.Name).ID;

                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if (file != null && file.ContentLength > 0)
                {
                    pomiar.Zdjecie = System.Guid.NewGuid().ToString() + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Zdjecia/") + pomiar.Zdjecie);
                }

                db.Entry(pomiar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pomiar);
        }

        // GET: Pomiary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pomiar pomiar = db.Pomiary.Find(id);
            if (pomiar == null)
            {
                return HttpNotFound();
            }
            return View(pomiar);
        }

        // POST: Pomiary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pomiar pomiar = db.Pomiary.Find(id);
            db.Pomiary.Remove(pomiar);
            db.SaveChanges();
            return RedirectToAction("Index");
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
