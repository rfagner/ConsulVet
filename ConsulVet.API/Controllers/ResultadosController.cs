using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        /// <summary>
        /// Cadastra resultados na aplicação
        /// </summary>
        /// <param name="resultado">Dados do resultado</param>
        /// <returns>Dados do resultado cadastrado na aplicação</returns>
        [HttpPost]
        public IActionResult Cadastrar(Resultado resultado)
        {
            try
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
                        cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = resultado.ClienteId;
                        cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = resultado.VeterinarioId;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }

        // GET - Consultar
        /// <summary>
        /// Lista todos os resultados da aplicação
        /// </summary>
        /// <returns>Lista de resultados</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var resultados = new List<Resultado>();

                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "SELECT * FROM Resultado";

                    using(SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Ler todos os itens do script
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultados.Add(new Resultado
                                {
                                    Id = (int)reader[0],
                                    Diagnostico = (string)reader[1],
                                    ClienteId = (int)reader[2],
                                    VeterinarioId = (int)reader[3]
                                });
                            }
                        }
                    }

                }

                return Ok(resultados);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }

        // PUT - Alterar
        /// <summary>
        /// Altera os dados de um resultado
        /// </summary>
        /// <param name="id">Id do resultado</param>
        /// <param name="resultado">Todas as informações do resultado</param>
        /// <returns>Resultado alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Resultado resultado)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE Resultado SET Diagnostico=@Diagnostico, ClienteId=@ClienteId, VeterinarioId=@VeterinarioId WHERE Id=@id";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Diagnostico", SqlDbType.NVarChar).Value = resultado.Diagnostico;
                        cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = resultado.ClienteId;
                        cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = resultado.VeterinarioId;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        resultado.Id = id;
                    }
                }

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }

        // DELETE - Excluir
        /// <summary>
        /// Exclui um resultado da aplicação
        /// </summary>
        /// <param name="id">Id do resultado</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "DELETE FROM Resultado WHERE Id=@id";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;                        

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        
                    }
                }

                return Ok(new
                {
                    msg = "Resultado excluído com sucesso"
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }
    }
}
