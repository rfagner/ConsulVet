using System.ComponentModel.DataAnnotations;

namespace ConsulVet.API.Models
{
    public class Resultado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o resultado do diagnóstico")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "Informe o ID do Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Informe o ID do Veterinário")]
        public int VeterinarioId { get; set; }
        public virtual Veterinario Veterinario { get; set; }
    }
}
