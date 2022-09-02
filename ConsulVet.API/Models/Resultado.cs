using ConsulVet.API.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ConsulVet.API.Models
{
    public class Resultado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o resultado do diagnóstico")]
        public string Diagnostico { get; set; }
        private int _clienteId;

        [Required(ErrorMessage = "Informe o ID do Cliente")]
        public int ClienteId
        {
            get
            {
                return _clienteId;
            }
            set
            {
                // Carrega o objeto com os dados do Cliente
                _clienteId = value;
                ClienteRepository clienteRepository = new ClienteRepository();
                if (_clienteId != 0)
                {
                    Cliente = clienteRepository.GetById(_clienteId);
                }

            }
        }
        public virtual Cliente Cliente { get; set; }
        private int _veterinarioId;

        [Required(ErrorMessage = "Informe o ID do Veterinário")]
        public int VeterinarioId
        {
            get
            {
                return _veterinarioId;
            }
            set
            {
                // Carrega o objeto com os dados do Veterinário
                _veterinarioId = value;
                VeterinarioRepository veterinarioRepository = new VeterinarioRepository();
                if (_veterinarioId != 0)
                {
                    Veterinario = veterinarioRepository.GetById(_veterinarioId);
                }
            }
        }
        public virtual Veterinario Veterinario { get; set; }
    }
}
