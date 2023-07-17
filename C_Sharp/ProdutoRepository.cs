using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

#nullable disable
public class ProdutoRepository{
    private readonly string connectionString;

    public ProdutoRepository(string connectionString){
        this.connectionString = connectionString;
    }

    public List<Produto> GetAllProdutos(){
        List<Produto> produtos = new List<Produto>();

        using (MySqlConnection connection = new MySqlConnection(connectionString)){
            connection.Open();
            string query = "SELECT * FROM Produtos";

            using (MySqlCommand command = new MySqlCommand(query, connection)){
                using (MySqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        Produto produto = new Produto{
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString(),
                            Preco = Convert.ToDecimal(reader["Preco"])
                        };

                        produtos.Add(produto);
                    }
                }
            }
        }

        return produtos;
    }

    public void AddProduto(Produto produto){
        using (MySqlConnection connection = new MySqlConnection(connectionString)){
            connection.Open();
            string query = "INSERT INTO Produtos (Nome, Preco) VALUES (@Nome, @Preco)";

            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Preco", produto.Preco);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateProduto(Produto produto){
        using (MySqlConnection connection = new MySqlConnection(connectionString)){
            connection.Open();
            string query = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco WHERE Id = @Id";

            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Preco", produto.Preco);
                command.Parameters.AddWithValue("@Id", produto.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteProduto(int id){
        using (MySqlConnection connection = new MySqlConnection(connectionString)){
            connection.Open();
            string query = "DELETE FROM Produtos WHERE Id = @Id";

            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
