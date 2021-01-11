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

namespace MojeTreningi.Controllers
{
    public class CwiczenieWPlaniesController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();

        // GET: CwiczenieWPlanies
        public ActionResult Index()
        {
            var cwiczeniaWPlanie = db.CwiczeniaWPlanie.Include(c => c.Cwiczenie).Include(c => c.Zestaw);
            return View(cwiczeniaWPlanie.ToList());
        }

        // GET: CwiczenieWPlanies/Details/5
        public ActionResult Details(int? id)
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
            return View(cwiczenieWPlanie);
        }

        // GET: CwiczenieWPlanies/Create
        public ActionResult Create()
        {
            ViewBag.CwiczenieID = new SelectList(db.Cwiczenia, "ID", "Nazwa");
            ViewBag.ZestawID = new SelectList(db.Zestawy, "ID", "Nazwa");
            return View();
        }

        // POST: CwiczenieWPlanies/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ZestawID,CwiczenieID,Serie,Powtorzenia,PrzerwaPomiedzySeriamiMinuty,PrzerwaPomiedzySeriamiSekundy,PrzerwaPoCwiczeniuMinuty,PrzerwaPoCwiczeniuSekundy")] CwiczenieWPlanie cwiczenieWPlanie)
        {
            if (ModelState.IsValid)
            {
                db.CwiczeniaWPlanie.Add(cwiczenieWPlanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CwiczenieID = new SelectList(db.Cwiczenia, "ID", "Nazwa", cwiczenieWPlanie.CwiczenieID);
            ViewBag.ZestawID = new SelectList(db.Zestawy, "ID", "Nazwa", cwiczenieWPlanie.ZestawID);
            return View(cwiczenieWPlanie);
        }

        // GET: CwiczenieWPlanies/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CwiczenieID = new SelectList(db.Cwiczenia, "ID", "Nazwa", cwiczenieWPlanie.CwiczenieID);
            ViewBag.ZestawID = new SelectList(db.Zestawy, "ID", "Nazwa", cwiczenieWPlanie.ZestawID);
            return View(cwiczenieWPlanie);
        }

        // POST: CwiczenieWPlanies/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ZestawID,CwiczenieID,Serie,Powtorzenia,PrzerwaPomiedzySeriamiMinuty,PrzerwaPomiedzySeriamiSekundy,PrzerwaPoCwiczeniuMinuty,PrzerwaPoCwiczeniuSekundy")] CwiczenieWPlanie cwiczenieWPlanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cwiczenieWPlanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CwiczenieID = new SelectList(db.Cwiczenia, "ID", "Nazwa", cwiczenieWPlanie.CwiczenieID);
            ViewBag.ZestawID = new SelectList(db.Zestawy, "ID", "Nazwa", cwiczenieWPlanie.ZestawID);
            return View(cwiczenieWPlanie);
        }

        // GET: CwiczenieWPlanies/Delete/5
        public ActionResult Delete(int? id)
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
            return View(cwiczenieWPlanie);
        }

        // POST: CwiczenieWPlanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CwiczenieWPlanie cwiczenieWPlanie = db.CwiczeniaWPlanie.Find(id);
            db.CwiczeniaWPlanie.Remove(cwiczenieWPlanie);
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
