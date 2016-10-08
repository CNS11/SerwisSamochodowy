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
    public class ZuzyteCzesciController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZuzyteCzesci
        public ActionResult Index()
        {
            var zuzyteCzescis = db.ZuzyteCzescis.Include(z => z.Potwierdzenie_odbioru);
            return View(zuzyteCzescis.ToList());
        }

        // GET: ZuzyteCzesci/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZuzyteCzesci zuzyteCzesci = db.ZuzyteCzescis.Find(id);
            if (zuzyteCzesci == null)
            {
                return HttpNotFound();
            }
            return View(zuzyteCzesci);
        }

        // GET: ZuzyteCzesci/Create
        public ActionResult Create()
        {
            ViewBag.PotwierdzenieOdbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId");
            return View();
        }

        // POST: ZuzyteCzesci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCzesci,nazwaCzesci,ilosc,PotwierdzenieOdbioruRefId")] ZuzyteCzesci zuzyteCzesci)
        {
            if (ModelState.IsValid)
            {
                db.ZuzyteCzescis.Add(zuzyteCzesci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PotwierdzenieOdbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId", zuzyteCzesci.PotwierdzenieOdbioruRefId);
            return View(zuzyteCzesci);
        }

        // GET: ZuzyteCzesci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZuzyteCzesci zuzyteCzesci = db.ZuzyteCzescis.Find(id);
            if (zuzyteCzesci == null)
            {
                return HttpNotFound();
            }
            ViewBag.PotwierdzenieOdbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId", zuzyteCzesci.PotwierdzenieOdbioruRefId);
            return View(zuzyteCzesci);
        }

        // POST: ZuzyteCzesci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCzesci,nazwaCzesci,ilosc,PotwierdzenieOdbioruRefId")] ZuzyteCzesci zuzyteCzesci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zuzyteCzesci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PotwierdzenieOdbioruRefId = new SelectList(db.Potwierdzenie_odbioruDbSet, "IdPotwierdzenia_odbioru", "KlientRefId", zuzyteCzesci.PotwierdzenieOdbioruRefId);
            return View(zuzyteCzesci);
        }

        // GET: ZuzyteCzesci/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZuzyteCzesci zuzyteCzesci = db.ZuzyteCzescis.Find(id);
            if (zuzyteCzesci == null)
            {
                return HttpNotFound();
            }
            return View(zuzyteCzesci);
        }

        // POST: ZuzyteCzesci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZuzyteCzesci zuzyteCzesci = db.ZuzyteCzescis.Find(id);
            db.ZuzyteCzescis.Remove(zuzyteCzesci);
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
