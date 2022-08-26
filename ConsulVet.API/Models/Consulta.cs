using System;

namespace ConsulVet.API.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int VeterinarioId { get; set; }
        public virtual Veterinario Veterinario { get; set; }
    }
}
