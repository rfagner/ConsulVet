using System.ComponentModel.DataAnnotations;

namespace ConsulVet.API.Models
{
    public class Cliente
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

        [Required(ErrorMessage = "Informe o nome do Pet")]
        public string NomePet { get; set; }
    }
}
