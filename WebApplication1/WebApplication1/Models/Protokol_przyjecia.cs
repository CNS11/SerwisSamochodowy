using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Protokol_przyjecia
    {
        public Protokol_przyjecia()
        {
               this.PozycjeProtokoluPrzyjecia=new List<Pozycja_protokolu_przyjecia>();
               this.Potwierdzenie_odbioru = new List<Potwierdzenie_odbioru>();
        }
        [Key]
        public int IdProtokolu_przyjecia { get; set; }
        [Display(Name="Data przyjęcia")]
        public DateTime Data_przyjecia { get; set; }
        [Display(Name = "Czy gotowe")]
        public bool Czy_gotowe { get; set; }
        [Display(Name = "Czy odebrane")]
        public bool Czy_Odebrane { get; set; }
        public string KlientRefId { get; set; }
        [ForeignKey("KlientRefId")]
        public virtual ApplicationUser Klient { get; set; }
        public int SamochodRefId { get; set; }
        [ForeignKey("SamochodRefId")]
        public virtual Samochod Samochod { get; set; }
      //  public virtual Potwierdzenie_odbioru Potwierdzenie_odbioru { get; set; }
        public virtual ICollection<Potwierdzenie_odbioru> Potwierdzenie_odbioru { get; set; }

        public virtual ICollection<Pozycja_protokolu_przyjecia> PozycjeProtokoluPrzyjecia { get; set; }
      //  public virtual ICollection<Potwierdzenie_odbioru> PotwierdzenieOdbioru { get; set; }
    }

}