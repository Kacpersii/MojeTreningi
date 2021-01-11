using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojeTreningi.DAL;
using MojeTreningi.Models;
using PagedList;

namespace MojeTreningi.Controllers
{
    public class AtlasCwiczenController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();

        public ActionResult AtlasCwiczen()
        {
            var partie = db.PartieCiala;

            return View(partie);
        }

        // GET: AtlasCwiczen
        public ActionResult Index(int? id, string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NazwaSortParm = String.IsNullOrEmpty(sortOrder) ? "nazwa_desc" : "";
            ViewBag.PartiaSortParm = sortOrder == "Partia" ? "partia_desc" : "Partia";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var cwiczenia = from c in db.Cwiczenia select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                cwiczenia = cwiczenia.Where(c => c.Nazwa.Contains(searchString)
                                        || c.Opis.Contains(searchString)
                                        || c.PartiaCiala.Nazwa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nazwa_desc":
                    cwiczenia = cwiczenia.OrderByDescending(c => c.Nazwa);
                    break;
                case "Partia":
                    cwiczenia = cwiczenia.OrderBy(c => c.PartiaCiala.Nazwa);
                    break;
                case "partia_desc":
                    cwiczenia = cwiczenia.OrderByDescending(c => c.PartiaCiala.Nazwa);
                    break;
                default:
                    cwiczenia = cwiczenia.OrderBy(c => c.Nazwa);
                    break;
            }

            if (id != null)
            {
                cwiczenia = cwiczenia.Where(c => c.PartiaCialaID == id);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(cwiczenia.ToPagedList(pageNumber, pageSize));
            //return View(cwiczenia.ToList());
        }

        // GET: AtlasCwiczen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cwiczenie cwiczenie = db.Cwiczenia.Find(id);
            if (cwiczenie == null)
            {
                return HttpNotFound();
            }
            return View(cwiczenie);
        }

        // GET: AtlasCwiczen/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.PartiaCialaID = new SelectList(db.PartieCiala, "ID", "Nazwa");
            return View();
        }

        // POST: AtlasCwiczen/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,PartiaCialaID,Nazwa,Opis")] Cwiczenie cwiczenie)
        {
            if (ModelState.IsValid)
            {
                db.Cwiczenia.Add(cwiczenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartiaCialaID = new SelectList(db.PartieCiala, "ID", "Nazwa", cwiczenie.PartiaCialaID);
            return View(cwiczenie);
        }

        // GET: AtlasCwiczen/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cwiczenie cwiczenie = db.Cwiczenia.Find(id);
            if (cwiczenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartiaCialaID = new SelectList(db.PartieCiala, "ID", "Nazwa", cwiczenie.PartiaCialaID);
            return View(cwiczenie);
        }

        // POST: AtlasCwiczen/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,PartiaCialaID,Nazwa,Opis")] Cwiczenie cwiczenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cwiczenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartiaCialaID = new SelectList(db.PartieCiala, "ID", "Nazwa", cwiczenie.PartiaCialaID);
            return View(cwiczenie);
        }

        // GET: AtlasCwiczen/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cwiczenie cwiczenie = db.Cwiczenia.Find(id);
            if (cwiczenie == null)
            {
                return HttpNotFound();
            }
            return View(cwiczenie);
        }

        // POST: AtlasCwiczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cwiczenie cwiczenie = db.Cwiczenia.Find(id);
            db.Cwiczenia.Remove(cwiczenie);
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
