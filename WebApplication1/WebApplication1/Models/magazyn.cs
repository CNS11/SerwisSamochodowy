using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Magazyn
    {
        public Magazyn()
        {
           // this.Modele = new Modele();
        }
        [Key]
        public int IdCzesci { get; set; }
        [Display(Name = "Nazwa Części")]
        public string Nazwa { get; set; }
        [Display(Name = "Numer Seryjny")]
        public string Numer_Seryjny { get; set; }
        [Display(Name = "Cena zakupu")]
        public float Cena_Zakupu { get; set; }
        public int test { get; set; }
        [Display(Name = "Sugerowana cena sprzedaży")]
        public float SugCenaSprz { get; set; }
        [Display(Name = "Stan magazynowy")]
        public int StanMagazynowy { get; set; }
        //public virtual Czesci Czesci { get; set; }
        public int ModeleRefId { get; set; }
        [ForeignKey("ModeleRefId")]
        public virtual Modele Modele { get; set; }
    }
}