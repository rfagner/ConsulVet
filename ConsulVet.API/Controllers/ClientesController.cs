using ConsulVet.API.Models;
using ConsulVet.API.Repositories;
using ConsulVet.API.Utils;
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
        // Variável que liga os objetos da classe Repository
        private ClienteRepository repositorio = new ClienteRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra clientes na aplicação
        /// </summary>
        /// <param name="cliente">Dados do cliente</param>
        /// <returns>Dados do cliente cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cliente cliente, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if(uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                cliente.Imagem = uploadResultado;                
                #endregion

                repositorio.Insert(cliente);
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
                var clientes = repositorio.GetAll();
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
        public IActionResult Alterar([FromForm] int id, Cliente cliente, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                cliente.Imagem = uploadResultado;                
                #endregion

                var buscarCliente = repositorio.GetById(id);
                if(buscarCliente == null)
                {
                    return NotFound();
                }

                var clienteAlterado = repositorio.Update(id, cliente);
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
                var buscarCliente = repositorio.GetById(id);
                if (buscarCliente == null)
                {
                    return NotFound();
                }

                repositorio.Delete(id);

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
