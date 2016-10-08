using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Speech;
using WebApplication1.Models;
using System.Speech.Synthesis;

namespace WebApplication1.Controllers
{
    public class SamochodyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Samochody
        public ActionResult Index()
        {
            //var text = "ʐaba"; //fonetyczny zapis słowa "żaba"



            //PromptBuilder myPrompt = new PromptBuilder();



            //myPrompt.AppendTextWithPronunciation(text, text);//dodajemy słowo z wymową
            //var synth = new SpeechSynthesizer();
            //synth.Speak(myPrompt); // synteza
            return View(db.SamochodDbSet.ToList());
        }
        public ActionResult MojeSamochody()
        {
                            var x = User.Identity.Name;
                var q = (from s in db.Users where s.UserName == x select s.Id).First();
                var samochody = (from s in db.SamochodDbSet
                                 join pp in db.Protokol_przyjeciaDbSet on s.IdSamochodu equals pp.SamochodRefId
                                 join k in db.Users on pp.KlientRefId equals k.Id
                                 where (k.Id == q)
                                 select s).ToList();
                return View(samochody);
        }

        // GET: Samochody/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.SamochodDbSet.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            ViewBag.KLient=this.getKlienci();
            return View(samochod);
        }

        // GET: Samochody/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Samochody/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSamochodu,Marka,Model,Rocznik,Numer_rejestracyjny,Numer_VIN")] Samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.SamochodDbSet.Add(samochod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(samochod);
        }

        // GET: Samochody/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.SamochodDbSet.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
           
            return View(samochod);
        }

        // POST: Samochody/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSamochodu,Marka,Model,Rocznik,Numer_rejestracyjny,Numer_VIN")] Samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samochod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samochod);
        }

        // GET: Samochody/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.SamochodDbSet.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        // POST: Samochody/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samochod samochod = db.SamochodDbSet.Find(id);
            db.SamochodDbSet.Remove(samochod);
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
        public ApplicationUser getKlienci()
        {
//TODO:napisac pobieranie uzytkownika dla samochodu
            var q = (from s in db.SamochodDbSet
                     join pp in db.Protokol_przyjeciaDbSet on s.IdSamochodu equals pp.SamochodRefId
                     join k in db.Users on pp.KlientRefId equals k.Id //where(k.Id==p)
                     select k.Id).First();
            //       var q = (from s in db.SamochodDbSet join pp in db.Protokol_przyjeciaDbSet on s.IdSamochodu equals pp.SamochodRefId select new { pp.IdProtokolu_przyjecia });
            ApplicationUser wlasciciel = (ApplicationUser)db.Users.Find(q);
            return wlasciciel;

        }
    }
}
