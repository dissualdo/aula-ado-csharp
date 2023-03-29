using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebAula.Domain;

namespace WebAula.DAL
{
    /// <summary>
    /// Classe cliente responsável pelo acesso ao repositório
    /// </summary>
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString) {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Método utilizado para incluir um cliente 
        /// </summary>
        /// <param name="cliente">dados do cliente</param>
        /// <returns>true para sucesso na inclusão, false quando houver um erro no banco</returns>
        public bool InserirCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Cliente (Nome, Email) VALUES (@Nome, @Email);";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Email", cliente.Email);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Método que busca todos os clientes do banco
        /// </summary>
        /// <returns>todos os usuários</returns>
        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Aula.dbo.Cliente;";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string nome = (string)reader["Nome"].ToString();
                        string email = (string)reader["Email"].ToString();

                        Cliente cliente = new Cliente(id, nome, email);
                        clientes.Add(cliente);
                    }
                }
            }

            return clientes;
        }

        /// <summary>
        /// Método utilizado para atualizar os dados do cliente 
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <param name="cliente">dados para atualização</param>
        /// <returns>true para sucesso na inclusão, false quando houver um erro no banco</returns>
        public bool AtualizarCliente(int id, Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Cliente SET Nome = @Nome, Email = @Email WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Método utilizado para remover o registro de cliente do banco de dados
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>true para sucesso na inclusão, false quando houver um erro no banco</returns>
        public bool RemoverCliente(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Cliente WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
