using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Cadastra consultas na aplicação
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Dados da consulta cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {
            try
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
                        cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = consulta.ClienteId;
                        cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = consulta.VeterinarioId;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(consulta);
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
        /// Lista todas as consultas da aplicação
        /// </summary>
        /// <returns>Lista de consultas</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var consultas = new List<Consulta>();

                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "SELECT * FROM Consulta";

                    using(SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Ler todos os itens do script
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                consultas.Add(new Consulta
                                {
                                    Id = (int)reader[0],
                                    DataHora = (DateTime)reader[1],
                                    ClienteId = (int)reader[2],
                                    VeterinarioId = (int)reader[3]
                                });
                            }
                        }
                    }

                }

                return Ok(consultas);
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
        /// Altera os dados de uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="consulta">Todas as informações da consulta</param>
        /// <returns>Consulta alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta consulta)
        {
            try
            {
                // Abre uma conexão
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE Consulta SET DataHora=@DataHora, ClienteId=@ClienteId, VeterinarioId=@VeterinarioId WHERE Id=@id";

                    // Criamos o comando de execução no banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Fazemos as declarações das variáveis por parâmetros
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@DataHora", SqlDbType.DateTime).Value = consulta.DataHora;
                        cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = consulta.ClienteId;
                        cmd.Parameters.Add("@VeterinarioId", SqlDbType.Int).Value = consulta.VeterinarioId;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        consulta.Id = id;
                    }
                }

                return Ok(consulta);
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
        /// Exclui uma consulta da aplicação
        /// </summary>
        /// <param name="id">Id da consulta</param>
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

                    string script = "DELETE FROM Consulta WHERE Id=@id";

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
                    msg = "Consulta excluída com sucesso"
                });
            }
            catch (Exception ex)
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
