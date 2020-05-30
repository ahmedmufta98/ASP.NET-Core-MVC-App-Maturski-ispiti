using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspit
    {
        public int Id { get; set; }
        public int PredajePredmetID { get; set; }
        public PredajePredmet PredajePredmet { get; set; }
        public DateTime Datum { get; set; }
        public int SkolaId { get; set; }
        public Skola Skola { get; set; }
        public string Napomena { get; set; }
    }
}
