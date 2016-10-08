using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Pracownicy
    {
        public Pracownicy()
        {
            
        }
        [Key]
        public int IdPracownika { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stanowisko { get; set; }
        public DateTime Data_zatrudnienia { get; set; }

       
    }
}