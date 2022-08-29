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
    public class VeterinariosController : ControllerBase
    {
        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        // POST - Cadastrar
        /// <summary>
        /// Cadastra veterinários na aplicação
        /// </summary>
        /// <param name="veterinario">Dados do veterinário</param>
        /// <returns>Dados do veterinário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Veterinario veterinario)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "INSERT INTO Veterinario (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = veterinario.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = veterinario.Email;
                        cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = veterinario.Senha;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(veterinario);
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
        /// Lista todos os veterinários da aplicação
        /// </summary>
        /// <returns>Lista de veterinários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var veterinarios = new List<Veterinario>();

                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "SELECT * FROM Veterinario";

                    using(SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Ler todos os itens do script
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                veterinarios.Add(new Veterinario
                                {
                                    Id = (int)reader[0],
                                    Nome = (string)reader[1],
                                    Email = (string)reader[2],
                                    Senha = (string)reader[3]                                    
                                });
                            }
                        }
                    }

                }

                return Ok(veterinarios);
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
        /// Altera os dados de um veterinário
        /// </summary>
        /// <param name="id">Id do veterinário</param>
        /// <param name="veterinario">Todas as informações do veterinário</param>
        /// <returns>Veterinário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Veterinario veterinario)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE Veterinario SET Nome=@Nome, Email=@Email, Senha=@Senha WHERE Id=@id";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = veterinario.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = veterinario.Email;
                        cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = veterinario.Senha;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        veterinario.Id = id;
                    }
                }

                return Ok(veterinario);
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
        /// Exclui um veterinário da aplicação
        /// </summary>
        /// <param name="id">Id do veterinário</param>
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

                    string script = "DELETE FROM Veterinario WHERE Id=@id";

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
                    msg = "Veterinário excluído com sucesso"
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
