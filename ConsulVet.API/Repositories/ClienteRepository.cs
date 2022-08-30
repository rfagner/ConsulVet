using ConsulVet.API.Interfaces;
using ConsulVet.API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ConsulVet.API.Repositories
{
    public class ClienteRepository : IClienteRepository
    {

        // Criar string de conexão com o banco de dados
        readonly string connectionString = "Data Source=DESKTOP-S1VHG92\\SQLEXPRESS;Integrated Security=true;Initial Catalog=ConsulVet";

        public bool Delete(int id)
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
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if(linhasAfetadas == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ICollection<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Cliente";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Ler todos os itens do script
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Senha = (string)reader[3],
                                NomePet = (string)reader[4],
                                Imagem = (string)reader[5].ToString(),                                
                            });
                        }
                    }
                }

            }

            return clientes;    
        }

        public Cliente GetById(int id)
        {
            var cliente = new Cliente();

            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "SELECT * FROM Cliente WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Ler todos os itens do script
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cliente.Id = (int)reader[0];
                            cliente.Nome = (string)reader[1];
                            cliente.Email = (string)reader[2];
                            cliente.Senha = (string)reader[3];
                            cliente.NomePet = (string)reader[4];                            
                            cliente.Imagem = (string)reader[5];                            
                        }
                    }
                }

            }

            return cliente;
        }

        public Cliente Insert(Cliente cliente)
        {
            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "INSERT INTO Cliente (Nome, Email, Senha, NomePet, Imagem) VALUES (@Nome, @Email, @Senha, @NomePet, @Imagem)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetros
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@NomePet", SqlDbType.NVarChar).Value = cliente.NomePet;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = cliente.Imagem;
                    

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return cliente;
        }

        public Cliente Update(int id, Cliente cliente)
        {
            // Abre uma conexão
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Cliente SET Nome=@Nome, Email=@Email, Senha=@Senha, NomePet=@NomePet, Imagem=@Imagem WHERE Id=@id";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@NomePet", SqlDbType.NVarChar).Value = cliente.NomePet;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = cliente.Imagem;                    

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    cliente.Id = id;
                }
            }
            return cliente;
        }
    }
}
