using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Protokol_przyjeciaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Protokol_przyjecia
        public ActionResult Index()
        {
            var protokol_przyjeciaDbSet = db.Protokol_przyjeciaDbSet.Include(p => p.Klient).Include(p => p.Samochod);
            return View(protokol_przyjeciaDbSet.ToList());
        }
        public ActionResult OznaczJakoGotowe(int id)
        {
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            protokol_przyjecia.Czy_gotowe = true;
            db.SaveChanges();
            string wiadomosc=String.Format("Dzień dobry.\nTwój samochód {0} {1} o numerze rejestracyjnym {2} jest juz gotowy do odebrania",protokol_przyjecia.Samochod.Marka,protokol_przyjecia.Samochod.Model,protokol_przyjecia.Samochod.Numer_rejestracyjny);
            SendEmail(protokol_przyjecia.Klient.Email.ToString(),"Twoja naprawa została zakończona",wiadomosc );
          //  sendSms(protokol_przyjecia.Samochod.Marka.ToString(), protokol_przyjecia.Samochod.Model.ToString(), protokol_przyjecia.Samochod.Numer_rejestracyjny, protokol_przyjecia.Klient.Numer_telefonu.ToString());
            return RedirectToAction("Index");
        }
        public void OznaczJakoRozliczone(int id)
        {
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            protokol_przyjecia.Czy_Odebrane = true;
            db.SaveChanges();
        }
        public ActionResult MojeProtokoly()
        {
                var x = User.Identity.Name;
                var q = (from s in db.Users where s.UserName == x select s.Id).First();
            var protokol_przyjeciaDbSet = (from pp in  db.Protokol_przyjeciaDbSet join k in db.Users on pp.KlientRefId equals k.Id where (pp.KlientRefId==q) select pp ).Include(p => p.Klient).Include(p => p.Samochod);
            return View(protokol_przyjeciaDbSet.ToList());

                        //var dane_poz = (from pp in db.Protokol_przyjeciaDbSet
                        //    join ppp in db.Pozycja_protokolu_przyjeciaDbSet on pp.IdProtokolu_przyjecia equals ppp.Protokol_przyjeciaRefId
                        //    where(ppp.Protokol_przyjeciaRefId==idpoz)
                        //     select ppp);



        }

        // GET: Protokol_przyjecia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            if (protokol_przyjecia == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.KLient = this.getKlienci(protokol_przyjecia.KlientRefId);
            ViewBag.Pozycje = this.getPozProt(protokol_przyjecia.IdProtokolu_przyjecia);
            return View(protokol_przyjecia);
        }

        public ApplicationUser getKlienci(string id)
        {
           // var x = (from s in db.Protokol_przyjeciaDbSet )
           // var q = (from s in db.Users where s.UserName == x select s.Id).First();
           // var q=(from )
            var dane_klienta=(from pp in db.Protokol_przyjeciaDbSet
                                  join k in db.Users on pp.KlientRefId equals k.Id where(k.Id==id) select k.Id).First();
            ApplicationUser wlasciciel = (ApplicationUser)db.Users.Find(dane_klienta);
            
            return wlasciciel;

        }
        public List<Pozycja_protokolu_przyjecia> getPozProt(int idpoz)
        {
            var dane_poz = (from pp in db.Protokol_przyjeciaDbSet
                            join ppp in db.Pozycja_protokolu_przyjeciaDbSet on pp.IdProtokolu_przyjecia equals ppp.Protokol_przyjeciaRefId
                            where(ppp.Protokol_przyjeciaRefId==idpoz)
                             select ppp);
            List<Pozycja_protokolu_przyjecia> pozycje = dane_poz.ToList();
            return pozycje;
        }


        // GET: Protokol_przyjecia/Create
        [HttpGet]
        public ActionResult Create()
        {
            var q=(from modele in db.Markis.OrderBy(m=>m.NazwaMarki) select modele.IdMarki).First();
            var model=(from mod in db.Modeles
                       join mar in db.Markis on mod.MarkiRefId equals mar.IdMarki where (mod.MarkiRefId==q) select mod).ToList();
                            var x = User.Identity.Name;
                var kliencis = (from s in db.Users where s.UserName == x select s.Id).First();
            var mojesam=(from pp in db.Protokol_przyjeciaDbSet
                             join klient in db.Users on pp.KlientRefId equals klient.Id
                             join samochod in db.SamochodDbSet on pp.SamochodRefId equals samochod.IdSamochodu where(klient.Id==kliencis)select samochod).Distinct().ToList();

            
            //ViewBag.Samochod_Marka = new SelectList(db.Markis.OrderBy(m=>m.NazwaMarki), "IdMarki", "NazwaMarki");
            //ViewBag.Samochod_Model = new SelectList(model, "NazwaModelu", "NazwaModelu");
            //ViewBag.Samochody = new SelectList(mojesam, "IdSamochodu", "dane");

            ViewBag.Samochod_Marka = new SelectList(db.Markis.OrderBy(m => m.NazwaMarki), "IdMarki", "NazwaMarki");
            ViewBag.Samochod_Model = new SelectList(model, "IdModelu", "NazwaModelu");
            ViewBag.Samochody = new SelectList(mojesam,"IdSamochodu", "dane");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetModele(int Marka)
        //public JsonResult GetModele(string Marka)
        {
              //  ApplicationDbContext db = new ApplicationDbContext();
          //  ApplicationDbContext db = new ApplicationDbContext();
                ApplicationDbContext db = new ApplicationDbContext();
                var model = (from mod in db.Modeles
                             join mar in db.Markis on mod.MarkiRefId equals mar.IdMarki
                             where (mod.MarkiRefId==Marka)
                             select new { mod.NazwaModelu }).ToList();

                //return new SelectList(model,"IdModelu", "NazwaModelu");
                      //  return new List<Modele>()=model;
                return Json(model,JsonRequestBehavior.AllowGet);
            
        }
        private static List<SelectListItem> GenerateNumbers()
        {
            var numbers = (from p in Enumerable.Range(0, 20)
                           select new SelectListItem
                           {
                               Text = p.ToString(),
                               Value = p.ToString()
                           });
            return numbers.ToList();
        }


        // POST: Protokol_przyjecia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProtokolu_przyjecia,Data_przyjecia,KlientRefId,SamochodRefId,Samochod")] Protokol_przyjecia protokol_przyjecia, string Usterka1, string Usterka2, string Usterka3, string Usterka1Uwagi, string Usterka2Uwagi, string Usterka3Uwagi, string Samochod_Marka, string Samochod_Model, string Czy_istnieje,string Samochody)
        {
            if (Czy_istnieje != "on")
            {


                protokol_przyjecia.Samochod.Marka = Samochod_Marka;
                protokol_przyjecia.Samochod.Model = Samochod_Model;
                var modelId = (from model in db.Modeles
                               join marki in db.Markis on model.MarkiRefId equals marki.IdMarki
                               where model.NazwaModelu == Samochod_Model && marki.NazwaMarki == Samochod_Marka
                               select model).First();
                try
                {                //db.SamochodDbSet.Add(new Samochod())
                    var x = User.Identity.Name;
                    var q = (from s in db.Users where s.UserName == x select s.Id).First();
                    //     protokol_przyjecia.Samochod = new Samochod();

                    protokol_przyjecia.KlientRefId = q;
                    protokol_przyjecia.Data_przyjecia = DateTime.Now;
                    // var samochod=db.SamochodDbSet.Add(new Samochod(protokol_przyjecia.Samochod.Marka, protokol_przyjecia.Samochod.Model, protokol_przyjecia.Samochod.Rocznik, protokol_przyjecia.Samochod.Numer_rejestracyjny, protokol_przyjecia.Samochod.Numer_VIN));
                    protokol_przyjecia.SamochodRefId = protokol_przyjecia.Samochod.IdSamochodu;//TODO:Napisac wstawianie samochodu
                    int IdModelu = (from model in db.Modeles
                                    join marki in db.Markis on model.MarkiRefId equals marki.IdMarki
                                    where (marki.NazwaMarki == Samochod_Marka && model.NazwaModelu == Samochod_Model)
                                    select model.IdModelu).First();
                    protokol_przyjecia.Samochod.ModelRefId = IdModelu;
                    db.Protokol_przyjeciaDbSet.Add(protokol_przyjecia);

                    db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka1, Usterka1Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));
                    if (Usterka2 != "") db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka2, Usterka2Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));
                    if (Usterka3 != "") db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka3, Usterka3Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));

                    db.SaveChanges();
                    return RedirectToAction("MojeProtokoly");
                }
                catch (Exception)
                {
                    return View(protokol_przyjecia);
                }
            }else
            {
                try
                {
                                        var x = User.Identity.Name;
                    var q = (from s in db.Users where s.UserName == x select s.Id).First();
                    //     protokol_przyjecia.Samochod = new Samochod();

                    protokol_przyjecia.KlientRefId = q;
                    protokol_przyjecia.Data_przyjecia = DateTime.Now;
                    int number = Convert.ToInt32(Samochody);
                    protokol_przyjecia.SamochodRefId = number;//TODO:Napisac wstawianie samochodu
                    db.Protokol_przyjeciaDbSet.Add(protokol_przyjecia);

                    db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka1, Usterka1Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));
                    if (Usterka2 != "") db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka2, Usterka2Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));
                    if (Usterka3 != "") db.Pozycja_protokolu_przyjeciaDbSet.Add(new Pozycja_protokolu_przyjecia(Usterka3, Usterka3Uwagi, protokol_przyjecia.IdProtokolu_przyjecia));

                    db.SaveChanges();
                    return RedirectToAction("MojeProtokoly");
                }
                catch (Exception)
                {
                    
                    return View(protokol_przyjecia);
                }
            }
        }

        // GET: Protokol_przyjecia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            if (protokol_przyjecia == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlientRefId = new SelectList(db.Users, "Id", "Imie", protokol_przyjecia.KlientRefId);
            ViewBag.SamochodRefId = new SelectList(db.SamochodDbSet, "IdSamochodu", "Marka", protokol_przyjecia.SamochodRefId);
            return View(protokol_przyjecia);
        }

        // POST: Protokol_przyjecia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProtokolu_przyjecia,Data_przyjecia,KlientRefId,SamochodRefId")] Protokol_przyjecia protokol_przyjecia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(protokol_przyjecia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlientRefId = new SelectList(db.Users, "Id", "Imie", protokol_przyjecia.KlientRefId);
            ViewBag.SamochodRefId = new SelectList(db.SamochodDbSet, "IdSamochodu", "Marka", protokol_przyjecia.SamochodRefId);
            return View(protokol_przyjecia);
        }

        // GET: Protokol_przyjecia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            if (protokol_przyjecia == null)
            {
                return HttpNotFound();
            }
            return View(protokol_przyjecia);
        }

        // POST: Protokol_przyjecia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Protokol_przyjecia protokol_przyjecia = db.Protokol_przyjeciaDbSet.Find(id);
            db.Protokol_przyjeciaDbSet.Remove(protokol_przyjecia);
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
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetListViaJson()
        {
            return Json(GenerateNumbers());
        }
        public void SendEmail(string EmailTo, string Subject, string Body)
        {
            string EmailFrom = "testprojektbazydanycprz@gmail.com";
            using (MailMessage mail = new MailMessage(EmailFrom, EmailTo))
            {
                mail.Subject = Subject;
                mail.Body = Body;
                mail.From = new MailAddress(EmailFrom, "Serwis samomochodowy");
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential netCred = new NetworkCredential(EmailFrom, "politechnika3ef");

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = netCred;
                smtp.Port = 587;
                smtp.Send(mail);
            }

        }
        //public void sendSms(string marka,string model,string numer_rej, string number)
        //{


        //    try
        //    {
        //        SMSApi.Api.Client client = new SMSApi.Api.Client("lukaszszeszol21aa@wp.pl");
        //        //client.SetPasswordHash("flatron1");
        //        client.SetPasswordRAW("bazydanych");

        //        var smsApi = new SMSApi.Api.SMSFactory(client);

        //        var result =
        //            smsApi.ActionSend()
        //                .SetText("Twój samochód "+marka+" "+model+" o numerze rejestracyjnym "+numer_rej+" jest już gotowy do odebrania!")
        //                .SetTo(number)
        //                .SetSender("Info") //Pole nadawcy lub typ wiadomość 'ECO', '2Way'
        //                .Execute();

        //        System.Console.WriteLine("Send: " + result.Count);
        //    }
        //    catch (SMSApi.Api.ActionException e)
        //    {
        //        /**
        //         * Błędy związane z akcją (z wyłączeniem błędów 101,102,103,105,110,1000,1001 i 8,666,999,201)
        //         * http://www.smsapi.pl/sms-api/kody-bledow
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.ClientException e)
        //    {
        //        /**
        //         * 101 Niepoprawne lub brak danych autoryzacji.
        //         * 102 Nieprawidłowy login lub hasło
        //         * 103 Brak punków dla tego użytkownika
        //         * 105 Błędny adres IP
        //         * 110 Usługa nie jest dostępna na danym koncie
        //         * 1000 Akcja dostępna tylko dla użytkownika głównego
        //         * 1001 Nieprawidłowa akcja
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.HostException e)
        //    {
        //        /* błąd po stronie servera lub problem z parsowaniem danych
        //         * 
        //         * 8 - Błąd w odwołaniu
        //         * 666 - Wewnętrzny błąd systemu
        //         * 999 - Wewnętrzny błąd systemu
        //         * 201 - Wewnętrzny błąd systemu
        //         * SMSApi.Api.HostException.E_JSON_DECODE - problem z parsowaniem danych
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.ProxyException e)
        //    {
        //        // błąd w komunikacji pomiedzy klientem a serverem
        //        System.Console.WriteLine(e.Message);
        //    }
        //}
        //public JsonResult NrRejValidation(string NrRej)
        //{
        //    var nRej = (from nr in db.SamochodDbSet where nr.Numer_rejestracyjny==NrRej select nr.Numer_rejestracyjny);

        //    if (nRej != null && nRej.Count() > 0)
        //    {
        //        var zmienna = nRej.First();
        //                 if (zmienna.ToString() == NrRej.ToString())
        //            {
        //                return Json(false);
        //            }
        //            else return Json(true);
                
        //    }

        //        return Json(true);





            



        //}
        public JsonResult NrVINValidation(string NrVIN)
        {
            var nVIN = (from nr in db.SamochodDbSet where nr.Numer_VIN == NrVIN select nr.Numer_VIN);

           
            if (nVIN != null && nVIN.Count() > 0)
            {
                var zmienna=nVIN.First();

                if (zmienna.ToString() == NrVIN.ToString())
                {
                    return Json(false);
                }
                else return Json(true);

            }

            return Json(true);

        }
        public JsonResult NrRejValidation(string NrRej)
        {



            //using (Sklep.Models.ShopEntities8 dc = new ShopEntities8())

            //MessageBox.Show(Login.ToString());
            var nRej = (from nr in db.SamochodDbSet where nr.Numer_rejestracyjny == NrRej select nr.Numer_rejestracyjny);

            if (nRej != null && nRej.Count() > 0)
            {
                var zmienna = nRej.First();
                if (zmienna.ToString() == NrRej.ToString())
                {
                    return Json(false);
                }
                else return Json(true);

            }

            return Json(true);

        }

    }
}
