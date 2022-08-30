using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System;

namespace ConsulVet.API.Utils
{
    // Singleton -> Static
    public static class Upload
    {
        // Upload de arquivos
        public static string UploadFile(IFormFile arquivo, string[] extensoesPermitidas, string diretorio)
        {
            try
            {
                // Determinamos onde será salvo o arquivo
                var pasta = Path.Combine("StaticFiles", diretorio);
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                // Verificamos se existe um arquivo para ser salvo
                if(arquivo.Length > 0)
                {
                    // Pegamos o nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    // Validamos se a extensão é permitida
                    if (ValidarExtensao(extensoesPermitidas, nomeArquivo))
                    {
                        var extensao = RetornarExtensao(nomeArquivo);
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        // Salvamos efetivamente o arquivo na aplicação
                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }
                        return novoNome;
                    }
                }
                // Retorna vazio caso não consiga fazer o stream
                return "";
                
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
            
        }

        // Validar extensão de arquivo
        public static bool ValidarExtensao(string[] extensoesPermitidas, string nomeArquivo)
        {
            string extensao = RetornarExtensao(nomeArquivo);
            
            foreach(string ext in extensoesPermitidas)
            {
                if(ext == extensao)
                {
                    return true;
                }
            }

            return false;
        }

        // Remover arquivo

        // Retornar a extensão
        public static string RetornarExtensao(string nomeArquivo)
        {
            string[] dados = nomeArquivo.Split('.');
            return dados[dados.Length - 1];
        }
    }
}
