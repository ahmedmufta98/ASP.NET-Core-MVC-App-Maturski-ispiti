using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspitStavke
    {
        public int Id { get; set; }
        public int? Bodovi { get; set; }
        public int MaturskiIspitId { get; set; }
        public MaturskiIspit MaturskiIspit { get; set; }
        public int DodjeljenPredmetId { get; set; }
        public DodjeljenPredmet DodjeljenPredmet { get; set; }
        public bool Pristupio { get; set; }
    }
}
