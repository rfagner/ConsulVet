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
    public class ClientesController : ControllerBase
    {
        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        // POST - Cadastrar
        /// <summary>
        /// Cadastra clientes na aplicação
        /// </summary>
        /// <param name="cliente">Dados do cliente</param>
        /// <returns>Dados do cliente cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "INSERT INTO Cliente (Nome, Email, Senha, NomePet) VALUES (@Nome, @Email, @Senha, @NomePet)";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
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
        /// Lista todos os clientes da aplicação
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var clientes = new List<Cliente>();

                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "SELECT * FROM Cliente";

                    using(SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Ler todos os itens do script
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    Id = (int)reader[0],
                                    Nome = (string)reader[1],
                                    Email = (string)reader[2],
                                    Senha = (string)reader[3],
                                    NomePet = (string)reader[4]
                                });
                            }
                        }
                    }

                }

                return Ok(clientes);
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
        /// Altera os dados de um cliente da aplicação
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="cliente">Todas as informações do cliente</param>
        /// <returns>Cliente alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Cliente cliente)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE Cliente SET Nome=@Nome, Email=@Email, Senha=@Senha, NomePet=@NomePet WHERE Id=@id";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cliente.Email;
                        cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = cliente.Senha;
                        cmd.Parameters.Add("@NomePet", SqlDbType.NVarChar).Value = cliente.NomePet;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cliente.Id = id;
                    }
                }

                return Ok(cliente);
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
        /// Exclui um cliente da aplicação
        /// </summary>
        /// <param name="id">Id do cliente</param>
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

                    string script = "DELETE FROM Cliente WHERE Id=@id";

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
                    msg = "Cliente excluído com sucesso"
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
