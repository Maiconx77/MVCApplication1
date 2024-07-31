using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MVCApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MVCApplication1.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly string _connectionString; // Sua string de conexão com o banco de dados

        public ProdutosController()
        {
            // Configure sua string de conexão aqui (substitua pelos valores reais)
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=teste01;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT ProdutoId, Nome, Preco, QuantidadeEstoque FROM Produtos";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produto produto = new Produto
                            {
                                ProdutoId = Convert.ToInt32(reader["ProdutoId"]),
                                Nome = reader["Nome"].ToString(),
                                Preco = Convert.ToDecimal(reader["Preco"]),
                                QuantidadeEstoque = Convert.ToInt32(reader["QuantidadeEstoque"])
                            };
                            produtos.Add(produto);
                        }
                    }
                }
            }

            return View(produtos);
        }
    }
}
