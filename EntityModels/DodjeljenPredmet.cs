using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class DodjeljenPredmet
    {
        public int Id { get; set; }

        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }


        [ForeignKey(nameof(PredmetId))]
        public virtual Predmet Predmet { get; set; }
        public int PredmetId { get; set; }
    
        public int ZakljucnoKrajGodine { get; set; }
    }
}
