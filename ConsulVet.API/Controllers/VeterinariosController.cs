﻿using ConsulVet.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public IActionResult Cadastrar(Veterinario veterinario)
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

        // GET - Consultar

        // PUT - Alterar

        // DELETE - Excluir
    }
}
