using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Czesci
    {
        public Czesci()
        {
          //  this.PotwierdzeniaOdbioru = new HashSet<Potwierdzenie_odbioru>();
          //  this.Potwierdzenie_odbioru = new Potwierdzenie_odbioru();
           // this.Magazyn = new Magazyn();
        }
        [Key]
        public int IdCzesci { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public int PotwierdzenieOdbioruRefId { get; set; }
        [ForeignKey("PotwierdzenieOdbioruRefId")]
        public virtual Potwierdzenie_odbioru Potwierdzenie_odbioru { get; set; }
        public int MagazynRefId { get; set; }
        [ForeignKey("MagazynRefId")]
        public virtual Magazyn Magazyn { get; set; }

        //public virtual ICollection<Potwierdzenie_odbioru> PotwierdzeniaOdbioru { get; set; }
    }
}