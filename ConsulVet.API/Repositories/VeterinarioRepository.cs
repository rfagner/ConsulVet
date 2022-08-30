using ConsulVet.API.Interfaces;
using ConsulVet.API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ConsulVet.API.Repositories
{
    public class VeterinarioRepository : IVeterinarioRepository
    {

        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        public bool Delete(int id)
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
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public ICollection<Veterinario> GetAll()
        {
            var veterinarios = new List<Veterinario>();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Veterinario";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
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

            return veterinarios;
        }

        public Veterinario GetById(int id)
        {
            var veterinario = new Veterinario();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Veterinario WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Ler todos os itens do script
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            veterinario.Id = (int)reader[0];
                            veterinario.Nome = (string)reader[1];
                            veterinario.Email = (string)reader[2];
                            veterinario.Senha = (string)reader[3];                        
                        }
                    }
                }

            }

            return veterinario;
        }

        public Veterinario Insert(Veterinario veterinario)
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

            return veterinario;
        }

        public Veterinario Update(int id, Veterinario veterinario)
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

            return veterinario;
        }
    }
}
