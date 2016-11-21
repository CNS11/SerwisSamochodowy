using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Potwierdzenie_odbioruController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Potwierdzenie_odbioru
        public ActionResult Index()
        {
            var potwierdzenie_odbioruDbSet = db.Potwierdzenie_odbioruDbSet.Include(p => p.Klient).Include(p => p.ProtokolPrzyjecia).Include(p => p.Samochod);
            return View(potwierdzenie_odbioruDbSet.ToList());
        }

        // GET: Potwierdzenie_odbioru/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potwierdzenie_odbioru potwierdzenie_odbioru = db.Potwierdzenie_odbioruDbSet.Find(id);
            if (potwierdzenie_odbioru == null)
            {
                return HttpNotFound();
            }
            return View(potwierdzenie_odbioru);
        }
        public JsonResult GetPasująceCzesci(string Model,int IdProt)
        {

            var model = (from mag in db.Magazyn
                         join modele in db.Modeles on mag.ModeleRefId equals modele.IdModelu
                         where (modele.NazwaModelu == Model)
                         select modele).ToList();

            //return new SelectList(model,"IdModelu", "NazwaModelu");
            //  return new List<Modele>()=model;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        // GET: Potwierdzenie_odbioru/Create
    [HttpGet]
        public ActionResult Create(int id)
        {




            ViewBag.Samochod_Marka = new SelectList(db.Markis.OrderBy(m => m.NazwaMarki), "NazwaMarki", "NazwaMarki");
            var pracownicy = (from u in db.Users
                              join roles in db.RoleDbSet on u.Id equals roles.UserId
                              where (roles.RoleId == "1")
                              select u).ToList();
            var czesci = (from pp in db.Protokol_przyjeciaDbSet
                         join sam in db.SamochodDbSet on pp.SamochodRefId equals sam.IdSamochodu
                         join modele in db.Modeles on sam.ModelRefId equals modele.IdModelu
                         join modele_has_czesci in db.Modele_has_CzesciDbSet on modele.IdModelu equals modele_has_czesci.ModeleRefId
                         join magazyn in db.Magazyn on modele_has_czesci.MagazynRefId equals magazyn.IdCzesci
                         where(pp.IdProtokolu_przyjecia==id && sam.IdSamochodu==pp.SamochodRefId && magazyn.StanMagazynowy>0)
                         select magazyn).ToList();

            ViewBag.Pracownicy = new SelectList(pracownicy, "Id", "FullName", null);
            ViewBag.Czesci = new SelectList(czesci, "IdCzesci", "Nazwa", null);
            ViewBag.CzesciSzczegoły=czesci;
            ViewBag.IdPrzyjecia = id;
            

            return View();
        }

        // POST: Potwierdzenie_odbioru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "IdPotwierdzenia_odbioru,Wycena,Data_odbioru,KlientRefId,SamochodRefId,ProtokolPrzyjeciaRefId")] Potwierdzenie_odbioru potwierdzenie_odbioru, int IdPrzyjecia,Double Wycena,int czesc1 = 0, int ilosc1 = 0, int czesc2 = 0, int ilosc2 = 0, int czesc3 = 0, int ilosc3 = 0, int czesc4 = 0, int ilosc4 = 0, int czesc5 = 0, int ilosc5 = 0, int czesc6 = 0, int ilosc6 = 0, int czesc7 = 0, int ilosc7 = 0, int czesc8 = 0, int ilosc8 = 0, int czesc9 = 0, int ilosc9 = 0, int czesc10 = 0, int ilosc10 = 0, int czesc11 = 0, int ilosc11 = 0, int czesc12 = 0, int ilosc12 = 0, int czesc13 = 0, int ilosc13 = 0, int czesc14 = 0, int ilosc14 = 0)
        {
  
            if (ModelState.IsValid)
            {
                var klientRefId  = (from pp in db.Protokol_przyjeciaDbSet
                                join klient in db.Users on pp.KlientRefId equals klient.Id
                                    where (pp.IdProtokolu_przyjecia == IdPrzyjecia)
                                select klient.Id).First();

                var samochodREfId=(from pp in db.Protokol_przyjeciaDbSet
                                       join samochod in db.SamochodDbSet on pp.SamochodRefId equals samochod.IdSamochodu
                                   where (pp.IdProtokolu_przyjecia == IdPrzyjecia)
                                       select samochod.IdSamochodu).First();
                var protokolPrzyj = (from pp in db.Protokol_przyjeciaDbSet
                                     where (pp.IdProtokolu_przyjecia == IdPrzyjecia)
                                   select pp).First();

                protokolPrzyj.Czy_Odebrane = true;
                potwierdzenie_odbioru.KlientRefId = klientRefId;
                potwierdzenie_odbioru.PracownikRefId = klientRefId;
                potwierdzenie_odbioru.ProtokolPrzyjeciaRefId = IdPrzyjecia;
                potwierdzenie_odbioru.Data_odbioru = DateTime.Now;
                potwierdzenie_odbioru.SamochodRefId=samochodREfId;
                float wycenanowa = (float)Wycena;
                potwierdzenie_odbioru.Wycena = wycenanowa;
                #region czesci
                ZuzyteCzesci pozycja1 = new ZuzyteCzesci(czesc1, ilosc1, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc1 != 0) db.ZuzyteCzescis.Add(pozycja1);
                ZuzyteCzesci pozycja2 = new ZuzyteCzesci(czesc2, ilosc2, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc2 != 0) db.ZuzyteCzescis.Add(pozycja2);
                ZuzyteCzesci pozycja3 = new ZuzyteCzesci(czesc3, ilosc3, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc3 != 0) db.ZuzyteCzescis.Add(pozycja3);
                ZuzyteCzesci pozycja4 = new ZuzyteCzesci(czesc4, ilosc4, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc4 != 0) db.ZuzyteCzescis.Add(pozycja4);
                ZuzyteCzesci pozycja5 = new ZuzyteCzesci(czesc5, ilosc5, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc5 != 0) db.ZuzyteCzescis.Add(pozycja5);
                ZuzyteCzesci pozycja6 = new ZuzyteCzesci(czesc6, ilosc6, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc6 != 0) db.ZuzyteCzescis.Add(pozycja1);
                ZuzyteCzesci pozycja7 = new ZuzyteCzesci(czesc7, ilosc7, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc7 != 0) db.ZuzyteCzescis.Add(pozycja7);
                ZuzyteCzesci pozycja8 = new ZuzyteCzesci(czesc8, ilosc8, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc8 != 0) db.ZuzyteCzescis.Add(pozycja8);
                ZuzyteCzesci pozycja9 = new ZuzyteCzesci(czesc9, ilosc9, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc9 != 0) db.ZuzyteCzescis.Add(pozycja9);
                ZuzyteCzesci pozycja10 = new ZuzyteCzesci(czesc10, ilosc10, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc10 != 0) db.ZuzyteCzescis.Add(pozycja10);
                ZuzyteCzesci pozycja11 = new ZuzyteCzesci(czesc11, ilosc11, potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
                if (ilosc11 != 0) db.ZuzyteCzescis.Add(pozycja11);
                #endregion
                db.Potwierdzenie_odbioruDbSet.Add(potwierdzenie_odbioru);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                
                
                return RedirectToAction("Index","Protokol_przyjecia");
            }

            ViewBag.KlientRefId = new SelectList(db.Users, "Id", "Imie", potwierdzenie_odbioru.KlientRefId);
            ViewBag.IdPotwierdzenia_odbioru = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
            ViewBag.SamochodRefId = new SelectList(db.SamochodDbSet, "IdSamochodu", "Marka", potwierdzenie_odbioru.SamochodRefId);
            return View(potwierdzenie_odbioru);
        }

        // GET: Potwierdzenie_odbioru/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potwierdzenie_odbioru potwierdzenie_odbioru = db.Potwierdzenie_odbioruDbSet.Find(id);
            if (potwierdzenie_odbioru == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlientRefId = new SelectList(db.Users, "Id", "Imie", potwierdzenie_odbioru.KlientRefId);
            ViewBag.IdPotwierdzenia_odbioru = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
            ViewBag.SamochodRefId = new SelectList(db.SamochodDbSet, "IdSamochodu", "Marka", potwierdzenie_odbioru.SamochodRefId);
            return View(potwierdzenie_odbioru);
        }

        // POST: Potwierdzenie_odbioru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPotwierdzenia_odbioru,Wycena,Data_odbioru,KlientRefId,SamochodRefId,ProtokolPrzyjeciaRefId")] Potwierdzenie_odbioru potwierdzenie_odbioru)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potwierdzenie_odbioru).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlientRefId = new SelectList(db.Users, "Id", "Imie", potwierdzenie_odbioru.KlientRefId);
            ViewBag.IdPotwierdzenia_odbioru = new SelectList(db.Protokol_przyjeciaDbSet, "IdProtokolu_przyjecia", "KlientRefId", potwierdzenie_odbioru.IdPotwierdzenia_odbioru);
            ViewBag.SamochodRefId = new SelectList(db.SamochodDbSet, "IdSamochodu", "Marka", potwierdzenie_odbioru.SamochodRefId);
            return View(potwierdzenie_odbioru);
        }

        // GET: Potwierdzenie_odbioru/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potwierdzenie_odbioru potwierdzenie_odbioru = db.Potwierdzenie_odbioruDbSet.Find(id);
            if (potwierdzenie_odbioru == null)
            {
                return HttpNotFound();
            }
            return View(potwierdzenie_odbioru);
        }

        // POST: Potwierdzenie_odbioru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Potwierdzenie_odbioru potwierdzenie_odbioru = db.Potwierdzenie_odbioruDbSet.Find(id);
            db.Potwierdzenie_odbioruDbSet.Remove(potwierdzenie_odbioru);
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
