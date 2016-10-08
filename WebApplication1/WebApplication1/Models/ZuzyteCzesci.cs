using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ZuzyteCzesci
    {
        private int? czesc1;
        private int? ilosc1;
        private int p;

        public ZuzyteCzesci(int id,int ilosc,int nrpotw)
        {
            this.IdCzesci = id;
            this.ilosc = ilosc;
            this.PotwierdzenieOdbioruRefId = nrpotw;
        }

 
        [Key]
        public int Id { get; set; }
        public int IdCzesci { get; set; }
        public int ilosc { get; set; }
        public int PotwierdzenieOdbioruRefId { get; set; }
        [ForeignKey("PotwierdzenieOdbioruRefId")]
        public virtual Potwierdzenie_odbioru Potwierdzenie_odbioru { get; set; }
    }
}