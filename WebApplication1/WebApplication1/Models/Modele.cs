using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Modele
    {
        public Modele()
        {
          //  this.Marki = new Marki();
            this.Magazyn = new List<Magazyn>();
        }
        [Key]
        [Required]
        public int IdModelu { get; set; }
        //[Required]
        //public int IdMarki { get; set; }
        [Required]
        public string NazwaModelu { get; set; }
        public int MarkiRefId { get; set; }
        [ForeignKey("MarkiRefId")]
        public virtual Marki Marki { get; set; }
        public virtual ICollection<Magazyn> Magazyn { get; set; }
    }
}