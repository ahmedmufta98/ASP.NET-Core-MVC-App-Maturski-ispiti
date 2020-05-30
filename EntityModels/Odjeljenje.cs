using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Odjeljenje
    {
        public int Id { get; set; }
        [ForeignKey(nameof(SkolaID))]
        public virtual Skola Skola { get; set; }
        public int SkolaID { get; set; }


        [ForeignKey(nameof(SkolskaGodinaID))]
        public virtual SkolskaGodina SkolskaGodina { get; set; }
        public int SkolskaGodinaID { get; set; }


        [ForeignKey(nameof(RazrednikID))]
        public virtual Nastavnik Razrednik { get; set; }
        public int RazrednikID { get; set; }


        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public bool IsPrebacenuViseOdjeljenje { get; set; }
    }
}
