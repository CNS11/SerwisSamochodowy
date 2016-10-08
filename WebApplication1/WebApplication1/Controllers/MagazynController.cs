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
    public class MagazynController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Magazyn
        public ActionResult Index()
        {
            return View(db.Magazyn.ToList());
        }

        // GET: Magazyn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazyn magazyn = db.Magazyn.Find(id);
            if (magazyn == null)
            {
                return HttpNotFound();
            }
            return View(magazyn);
        }

        // GET: Magazyn/Create
        public ActionResult Create()
        {
            var q = (from modele in db.Modeles
                     join marki in db.Markis on modele.MarkiRefId equals marki.IdMarki
                     select modele).Distinct().ToList();
          //  q=q.Distinct(p=>p.)
            ViewBag.Model = new SelectList(q, "IdModelu", "NazwaModelu", null);

            return View();
        }

        // POST: Magazyn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCzesci,StanMagazynowy,Nazwa,Numer_Seryjny,CenaZakupu,SugCenaSprz")] Magazyn magazyn, int Model)
        {
            if (ModelState.IsValid)
            {
                magazyn.ModeleRefId = Model;
                db.Magazyn.Add(magazyn);
                Modele_has_Czesci pozycja = new Modele_has_Czesci(magazyn.IdCzesci, Model);
                db.Modele_has_CzesciDbSet.Add(pozycja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magazyn);
        }

        // GET: Magazyn/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazyn magazyn = db.Magazyn.Find(id);
            var modelczesc=(from czesc in db.Magazyn
            join model in db.Modeles on czesc.ModeleRefId equals model.IdModelu
                                where czesc.IdCzesci==id select model.IdModelu).First();
            var q = (from modele in db.Modeles
                     join marki in db.Markis on modele.MarkiRefId equals marki.IdMarki
                     select modele).Distinct().ToList();
            //  q=q.Distinct(p=>p.)
            ViewBag.Model = new SelectList(q, "IdModelu", "NazwaModelu",modelczesc);
            if (magazyn == null)
            {
                return HttpNotFound();
            }
            ViewBag.idModelu = modelczesc;
            return View(magazyn);
        }

        // POST: Magazyn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCzesci,Nazwa,Numer_Seryjny,Cena_Zakupu,StanMagazynowy,ModeleRefId,SugCenaSprz")] Magazyn magazyn, int Model)
        {
            if (ModelState.IsValid)
            {
                //var q = (from czesci in db.Magazyn where czesci.IdCzesci == magazyn.IdCzesci select czesci).First();
                //magazyn.Cena_Zakupu = q.Cena_Zakupu;
                //magazyn.SugCenaSprz = q.SugCenaSprz;
                //magazyn.ModeleRefId = idModelu;
                magazyn.ModeleRefId =Model;
                try
                {
                    db.Entry(magazyn).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                    throw;
                }

                return RedirectToAction("Index");
            }
            return View(magazyn);
        }

        // GET: Magazyn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazyn magazyn = db.Magazyn.Find(id);
            if (magazyn == null)
            {
                return HttpNotFound();
            }
            return View(magazyn);
        }

        // POST: Magazyn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magazyn magazyn = db.Magazyn.Find(id);
            db.Magazyn.Remove(magazyn);
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
