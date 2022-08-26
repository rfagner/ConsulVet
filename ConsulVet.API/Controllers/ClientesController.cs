using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ConsulVet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        // POST - Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            // Abre uma conexão
            using(SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "INSERT INTO Cliente (Nome, Email, Senha, NomePet) VALUES (@Nome, @Email, @Senha, @NomePet)";

                // Criamos o comando de execução no banco
                using(SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetros
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@NomePet", SqlDbType.NVarChar).Value = cliente.NomePet;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(cliente);
        }

        // GET - Consultar

        // PUT - Alterar

        // DELETE - Excluir
    }
}
