using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Pozycja_protokolu_przyjecia
    {
        public Pozycja_protokolu_przyjecia()
        {

        }
        public Pozycja_protokolu_przyjecia(string opis,string uwagi,int prot)
        {
            this.Opis_usterki = opis;
            this.Uwagi = uwagi;
            this.Protokol_przyjeciaRefId = prot;
           // this.Potwierdzenie_odbioruRefId = podb;
        }
        [Key]
        public int IdPozycji_protokolu_przyjecia { get; set; }
        [Display(Name="Opis usterki")]
        public string Opis_usterki { get; set; }
        public string Uwagi { get; set; }
        public int  Protokol_przyjeciaRefId { get; set; }
        [ForeignKey("Protokol_przyjeciaRefId")]
        public virtual Protokol_przyjecia Protokol_przyjecia { get; set; }
        //public int Potwierdzenie_odbioruRefId { get; set; }
        //[ForeignKey("Potwierdzenie_odbioruRefId")]
        //public virtual Potwierdzenie_odbioru Potwierdzenie_odbioru { get; set; }
    }
}