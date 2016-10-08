using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Pozycja_protokolu_przyjeciaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pozycja_protokolu_przyjecia
        public ActionResult Index()
        {
            var pozycja_protokolu_przyjeciaDbSet = db.Pozycja_protokolu_przyjeciaDbSet.Include(p => p.Protokol_przyjecia);
            return View(pozycja_protokolu_przyjeciaDbSet.ToList());
        }

        // GET: Pozycja_protokolu_przyjecia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia = db.Pozycja_protokolu_przyjeciaDbSet.Find(id);
            if (pozycja_protokolu_przyjecia == null)
            {
                return HttpNotFound();
            }
            return View(pozycja_protokolu_przyjecia);
        }

        // GET: Pozycja_protokolu_przyjecia/Create
        public ActionResult Create()
        {
            ViewBag.Potwierdzenie_odbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId");
            ViewBag.Protokol_przyjeciaRefId = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId");
            return View();
        }

        // POST: Pozycja_protokolu_przyjecia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPozycji_protokolu_przyjecia,Opis_usterki,Uwagi,Protokol_przyjeciaRefId,Potwierdzenie_odbioruRefId")] Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia)
        {
            if (ModelState.IsValid)
            {
                db.Pozycja_protokolu_przyjeciaDbSet.Add(pozycja_protokolu_przyjecia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

         //   ViewBag.Potwierdzenie_odbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId", pozycja_protokolu_przyjecia.Potwierdzenie_odbioruRefId);
            ViewBag.Protokol_przyjeciaRefId = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", pozycja_protokolu_przyjecia.Protokol_przyjeciaRefId);
            return View(pozycja_protokolu_przyjecia);
        }

        // GET: Pozycja_protokolu_przyjecia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia = db.Pozycja_protokolu_przyjeciaDbSet.Find(id);
            if (pozycja_protokolu_przyjecia == null)
            {
                return HttpNotFound();
            }

            ViewBag.Protokol_przyjeciaRefId = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", pozycja_protokolu_przyjecia.Protokol_przyjeciaRefId);
            return View(pozycja_protokolu_przyjecia);
        }

        // POST: Pozycja_protokolu_przyjecia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPozycji_protokolu_przyjecia,Opis_usterki,Uwagi,Protokol_przyjeciaRefId,Potwierdzenie_odbioruRefId")] Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pozycja_protokolu_przyjecia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         //   ViewBag.Potwierdzenie_odbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId", pozycja_protokolu_przyjecia.Potwierdzenie_odbioruRefId);
            ViewBag.Protokol_przyjeciaRefId = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", pozycja_protokolu_przyjecia.Protokol_przyjeciaRefId);
            return View(pozycja_protokolu_przyjecia);
        }

        // GET: Pozycja_protokolu_przyjecia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia = db.Pozycja_protokolu_przyjeciaDbSet.Find(id);
            if (pozycja_protokolu_przyjecia == null)
            {
                return HttpNotFound();
            }
            return View(pozycja_protokolu_przyjecia);
        }

        // POST: Pozycja_protokolu_przyjecia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pozycja_protokolu_przyjecia pozycja_protokolu_przyjecia = db.Pozycja_protokolu_przyjeciaDbSet.Find(id);
            db.Pozycja_protokolu_przyjeciaDbSet.Remove(pozycja_protokolu_przyjecia);
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
