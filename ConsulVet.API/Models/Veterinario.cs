using System.ComponentModel.DataAnnotations;

namespace ConsulVet.API.Models
{
    public class Veterinario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres")]
        public string Senha { get; set; }
        public string Imagem { get; set; }
    }
}
