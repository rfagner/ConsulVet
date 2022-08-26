using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ConsulVet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        // POST - Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {
            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "INSERT INTO Consulta (DataHora, ClienteId, VeterinarioId) VALUES (@DataHora, @ClienteId, @VeterinarioId)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetros
                    cmd.Parameters.Add("@DataHora", SqlDbType.DateTime).Value = consulta.DataHora;
                    cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = consulta.ClienteID;
                    cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = consulta.VeterinarioId;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(consulta);
        }

        // GET - Consultar

        // PUT - Alterar

        // DELETE - Excluir
    }
}
