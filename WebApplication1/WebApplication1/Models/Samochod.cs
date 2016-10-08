using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Samochod
    {
        public Samochod(string marka,string model,string rocznik,string Numer_rejestracyjny,string Numer_VIN)
        {
            this.Marka = marka;
            this.Model = model;
            this.Rocznik = rocznik;
            this.Numer_rejestracyjny = Numer_rejestracyjny;
            this.Numer_VIN = Numer_VIN;
            this.Protokoly_przyjecia = new HashSet<Protokol_przyjecia>();
            this.Potwierdzenia_odbioru = new HashSet<Potwierdzenie_odbioru>();
        }
        public Samochod ()
	{
            this.Protokoly_przyjecia = new HashSet<Protokol_przyjecia>();
            this.Potwierdzenia_odbioru = new HashSet<Potwierdzenie_odbioru>();
	}
        [Key]
        [Required]
        public int IdSamochodu { get; set; }
        [Required]
        public string Marka { get; set; }
        [Required]
        public string Model { get; set; }
        public string dane
        {
            get { return  Numer_rejestracyjny+ " " + Marka+" "+Model; }
        }
        [Required(ErrorMessage = "Proszę podać rocznik")]
        public string Rocznik { get; set; }
        [Display(Name="Nr. rejestracyjny")]
        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)]
        //[Index("Num_rej", 1, IsUnique = true)]
        public string Numer_rejestracyjny { get; set; }
        [Display(Name = "Nr. VIN")]
        [Required]
        //[Index("Num_VIN", 1, IsUnique = true)]
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Numer_VIN { get; set; }
        public int ModelRefId { get; set; }
        [ForeignKey("ModelRefId")]
        public virtual Modele Modele { get; set; }


        public virtual ICollection<Potwierdzenie_odbioru> Potwierdzenia_odbioru { get; set; }
        public virtual ICollection<Protokol_przyjecia> Protokoly_przyjecia { get; set; }
    }
}