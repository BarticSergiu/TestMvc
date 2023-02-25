using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvc.Models
{
    public class Pontaj
    {
        public int PontajId { get; set; }
        public int Data { get; set; }
        public int Durata { get; set; }

        [ForeignKey("SarcinaRefId")]
        public Sarcina Sarcina { get; set; }

        public string Observatii { get; set; }
    }
}
