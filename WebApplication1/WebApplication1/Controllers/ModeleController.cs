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
    public class ModeleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modele
        public ActionResult Index()
        {
            var modeles = (from m in db.Modeles.Include(m => m.Marki) orderby m.Marki.NazwaMarki,m.NazwaModelu select m);
            return View(modeles.ToList());
        }

        // GET: Modele/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // GET: Modele/Create
        public ActionResult Create()
        {
            ViewBag.MarkiRefId = new SelectList(db.Markis, "IdMarki", "NazwaMarki");
            return View();
        }

        // POST: Modele/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NazwaModelu,MarkiRefId")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Modeles.Add(modele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarkiRefId = new SelectList(db.Markis, "IdMarki", "NazwaMarki", modele.MarkiRefId);
            return View(modele);
        }

        // GET: Modele/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarkiRefId = new SelectList(db.Markis, "IdMarki", "NazwaMarki", modele.MarkiRefId);
            return View(modele);
        }

        // POST: Modele/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdModelu,NazwaModelu,MarkiRefId")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarkiRefId = new SelectList(db.Markis, "IdMarki", "NazwaMarki", modele.MarkiRefId);
            return View(modele);
        }

        // GET: Modele/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modele modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // POST: Modele/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modele modele = db.Modeles.Find(id);
            db.Modeles.Remove(modele);
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
