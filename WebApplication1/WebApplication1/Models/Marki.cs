using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Marki
    {
        [Key]
        [Required]
        public int IdMarki { get; set; }
        [Required]
        public string NazwaMarki { get; set; }

    }
}