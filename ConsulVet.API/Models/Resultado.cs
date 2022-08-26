namespace ConsulVet.API.Models
{
    public class Resultado
    {
        public int Id { get; set; }
        public string Diagnostico { get; set; }
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int VeterinarioID { get; set; }
        public virtual Veterinario Veterinario { get; set; }
    }
}
