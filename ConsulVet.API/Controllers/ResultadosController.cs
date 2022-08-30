using ConsulVet.API.Models;
using ConsulVet.API.Repositories;
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
        // Variável que liga os objetos da classe Repository
        private ResultadoRepository repositorio = new ResultadoRepository();

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
                repositorio.Insert(resultado);
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
                var resultados = repositorio.GetAll();
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
                var buscarResultado = repositorio.GetById(id);
                if(buscarResultado == null)
                {
                    return NotFound();
                }

                var resultadoAlterado = repositorio.Update(id, resultado);
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
                var buscarResultado = repositorio.GetById(id);
                if (buscarResultado == null)
                {
                    return NotFound();
                }

                repositorio.Delete(id);

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
