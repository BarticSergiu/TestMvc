using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvc.Models
{
    public class Persoana
{ 
        public int PersoanaId { get; set; }

    public string Nume { get; set; }

    public string Prenume { get; set; }

    public string Sex { get; set; }

    public ICollection<Persoana> Persoane { get; set; }
}
}