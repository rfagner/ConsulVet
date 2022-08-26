using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ConsulVet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        // POST - Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Resultado resultado)
        {
            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "INSERT INTO Resultado (Diagnostico, ClienteId, VeterinarioId) VALUES (@Diagnostico, @ClienteId, @VeterinarioId)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetros
                    cmd.Parameters.Add("@Diagnostico", SqlDbType.NVarChar).Value = resultado.Diagnostico;
                    cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = resultado.ClienteID;
                    cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = resultado.VeterinarioID;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(resultado);
        }

        // GET - Consultar

        // PUT - Alterar

        // DELETE - Excluir
    }
}
