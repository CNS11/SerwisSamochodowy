using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Potwierdzenie_odbioru
    {
        public Potwierdzenie_odbioru()
        {
            this.Czesci = new HashSet<Czesci>();
          //  this.Pozycje_protokolu_przyjecia = new HashSet<Pozycja_protokolu_przyjecia>();
           // this.ProtokolPrzyjecia = new Protokol_przyjecia();
            //this. = new HashSet<ApplicationUser>();
            //this.Pracownik = new ApplicationUser();
            //this.Klient=new ApplicationUser();
        }
        [Key]

        public int IdPotwierdzenia_odbioru{get;set;}
        [DataType(DataType.Currency)]
        [Required]
        public float Wycena { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Data_odbioru { get; set; }
        public string KlientRefId { get; set; }
        [ForeignKey("KlientRefId")]
        public virtual ApplicationUser Klient { get; set; }
        public int SamochodRefId { get; set; }
        [ForeignKey("SamochodRefId")]
        public virtual Samochod Samochod { get; set; }
        public int ProtokolPrzyjeciaRefId { get; set; }
        [ForeignKey("ProtokolPrzyjeciaRefId")]
        public virtual Protokol_przyjecia ProtokolPrzyjecia { get; set; }
        public string PracownikRefId { get; set; }
        [ForeignKey("PracownikRefId")]
        public virtual ApplicationUser Pracownik { get; set; }

        public virtual ICollection<Czesci> Czesci { get; set; }
       // public virtual ICollection<Pozycja_protokolu_przyjecia> Pozycje_protokolu_przyjecia { get; set; }


    }
}