using ConsulVet.API.Interfaces;
using ConsulVet.API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ConsulVet.API.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {

        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        public bool Delete(int id)
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
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public ICollection<Consulta> GetAll()
        {
            var consultas = new List<Consulta>();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Consulta";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
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

            return consultas;
        }

        public Consulta GetById(int id)
        {
            var consulta = new Consulta();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Consulta WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Ler todos os itens do script
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consulta.Id = (int)reader[0];
                            consulta.DataHora = (DateTime)reader[1];
                            consulta.ClienteId = (int)reader[2];
                            consulta.VeterinarioId = (int)reader[3];                            
                        }
                    }
                }

            }

            return consulta;
        }

        public Consulta Insert(Consulta consulta)
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

            return consulta;
        }

        public Consulta Update(int id, Consulta consulta)
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
            return consulta;
        }
    }
}
