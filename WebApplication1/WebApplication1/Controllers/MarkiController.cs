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
    public class MarkiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Marki
        public ActionResult Index()
        {
            return View(db.Markis.ToList());
        }

        // GET: Marki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Markis.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // GET: Marki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMarki,NazwaMarki")] Marki marki)
        {
            if (ModelState.IsValid)
            {
                db.Markis.Add(marki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marki);
        }

        // GET: Marki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Markis.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // POST: Marki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMarki,NazwaMarki")] Marki marki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marki);
        }

        // GET: Marki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Markis.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // POST: Marki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marki marki = db.Markis.Find(id);
            db.Markis.Remove(marki);
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
