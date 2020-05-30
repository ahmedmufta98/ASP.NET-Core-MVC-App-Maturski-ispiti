using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_Ispit_asp.net_core.EntityModels
{
   
    public class PredajePredmet
    {
        public int Id { get; set; }

        [ForeignKey(nameof(PredmetID))]
        public virtual Predmet Predmet { get; set; }
        public int PredmetID { get; set; }


        [ForeignKey(nameof(OdjeljenjeID))]
        public virtual Odjeljenje Odjeljenje { get; set; }
        public int OdjeljenjeID { get; set; }


        [ForeignKey(nameof(NastavnikID))]
        public virtual Nastavnik Nastavnik { get; set; }
        public int NastavnikID { get; set; }

    }
}
