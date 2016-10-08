using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Modele_has_Czesci
    {
        public Modele_has_Czesci(int idCzesci,int idModelu)
        {
            this.ModeleRefId = idModelu;
            this.MagazynRefId = idCzesci;
        }

        [Key, Column(Order = 0)]
        public int ModeleRefId { get; set; }
        [ForeignKey("ModeleRefId")]
        public virtual Modele Modele { get; set; }
        [Key, Column(Order = 1)]
        public int MagazynRefId { get; set; }
        [ForeignKey("MagazynRefId")]
        public virtual Magazyn Magazyn { get; set; }
    }
}