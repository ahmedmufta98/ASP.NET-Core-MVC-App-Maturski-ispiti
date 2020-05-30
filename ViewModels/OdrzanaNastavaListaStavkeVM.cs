using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaListaStavkeVM
    {
        public int Id { get; set; }
        public List<Stavke> MatStavke { get; set; }
        public class Stavke
        {
            public int StavkaId { get; set; }
            public string Ucenik { get; set; }
            public double Prosjek { get; set; }
            public int? Bodovi { get; set; }
            public bool Pristupio { get; set; }
        }
    }

    
}
