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
    public class VeterinariosController : ControllerBase
    {
        // Variável que liga os objetos da classe Repository
        private VeterinarioRepository repositorio = new VeterinarioRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra veterinários na aplicação
        /// </summary>
        /// <param name="veterinario">Dados do veterinário</param>
        /// <returns>Dados do veterinário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Veterinario veterinario, IFormFile arquivo)
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

                veterinario.Imagem = uploadResultado;
                #endregion

                repositorio.Insert(veterinario);
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
                var veterinarios = repositorio.GetAll();
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
        public IActionResult Alterar([FromForm] int id, Veterinario veterinario, IFormFile arquivo)
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

                veterinario.Imagem = uploadResultado;
                #endregion

                var buscarVeterinario = repositorio.GetById(id);
                if (buscarVeterinario == null)
                {
                    return NotFound();
                }

                var veterinarioAlterado = repositorio.Update(id, veterinario);
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
                var buscarVeterinario = repositorio.GetById(id);
                if (buscarVeterinario == null)
                {
                    return NotFound();
                }

                repositorio.Delete(id);

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
