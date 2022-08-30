using ConsulVet.API.Interfaces;
using ConsulVet.API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ConsulVet.API.Repositories
{
    public class ResultadoRepository : IResultadoRepository
    {

        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        public bool Delete(int id)
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
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public ICollection<Resultado> GetAll()
        {
            var resultados = new List<Resultado>();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Resultado";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
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

            return resultados;
        }

        public Resultado GetById(int id)
        {
            var resultado = new Resultado();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Resultado WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Ler todos os itens do script
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultado.Id = (int)reader[0];
                            resultado.Diagnostico = (string)reader[1];
                            resultado.ClienteId = (int)reader[2];
                            resultado.VeterinarioId = (int)reader[3];                           
                        }
                    }
                }

            }

            return resultado;
        }

        public Resultado Insert(Resultado resultado)
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

            return resultado;
        }

        public Resultado Update(int id, Resultado resultado)
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
            return resultado;
        }
    }
}
