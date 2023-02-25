using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvc.Models
{
    public class Sarcina
    {
        public int SarcinaId { get; set; }

        public string Denumire { get; set; }

        public string Descriere { get; set; }

        public string Prioritate { get; set; }

        public string TipSarcina { get; set; }

        public int OreEstimate { get; set; }

        [ForeignKey("PersoanaRefId")]
        public Persoana Persoana { get; set; }
    }
}