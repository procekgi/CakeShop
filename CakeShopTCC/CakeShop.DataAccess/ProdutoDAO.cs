using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ProdutoDAO
    {
        public void Inserir(Produto obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
                                  values (@Nome_Produto, @Preco, @Id_UnidadeDeMedida, @Id_Categoria, @Descricao);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@Nome_Produto", SqlDbType.VarChar).Value = obj.Nome_Produto;
                    cmd.Parameters.Add("@Preco", SqlDbType.Decimal).Value = obj.Preco;
                    cmd.Parameters.Add("@Id_UnidadeDeMedida", SqlDbType.Int).Value = obj.UnidadeDeMedida.Id_UnidadeDeMedida;
                    cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = obj.Categoria.Id_Categoria;
                    cmd.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = obj.Descricao;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public List<Produto> BuscarTodos()
        {
            var lst = new List<Produto>();
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
                                    u.Id as Id_UnidadeDeMedida,
                                    u.Nome as Nome_Unidade, 
                                    c.Id as Id_Categoria, 
                                    c.Nome as Nome_Categoria, 
                                    p.Id_Produto,
                                    p.Nome_Produto, 
                                    p.Preco, 
                                    p.Descricao 
                                from Produto p
                                inner join UnidadeDeMedida u on (p.Id_UnidadeDeMedida = u.Id)
                                inner join Categoria c on (p.Id_Categoria = c.Id);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var produto = new Produto()
                        {
                            Id_Produto = Convert.ToInt32(row["Id_Produto"]),
                            Nome_Produto = row["Nome_Produto"].ToString(),
                            Preco = Convert.ToDecimal(row["Preco"]),
                            UnidadeDeMedida = new UnidadeDeMedida
                            {
                                Id_UnidadeDeMedida = Convert.ToInt32(row["Id_UnidadeDeMedida"]),
                                Nome = row["Nome_Unidade"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                Id_Categoria = Convert.ToInt32(row["Id_Categoria"]),
                                Nome_Categoria = row["Nome_Categoria"].ToString()
                            },
                            Descricao = row["descricao"].ToString()
                        };

                        lst.Add(produto);
                    }
                }
            }

            return lst;
        }

        public List<Produto> BuscarPorCategoria(int categoriaId)
        {
            var lst = new List<Produto>();
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=CakeShop;
                            Data Source=localhost;
                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select u.Nome, c.Nome, p.Nome_Produto, p.Preco, p.Descricao from Produto p
                                inner join in UnidadeDeMedida u on (p.Id_UnidadeDeMedida = u.Id)
                                inner join in Categoria c on (p.Id_Categoria = c.Id)
                                where p.Id_Categoria = @Id_Categoria;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = categoriaId;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var produto = new Produto()
                        {
                            Id_Produto = Convert.ToInt32(row["id"]),
                            Nome_Produto = row["Nome_produto"].ToString(),
                            Preco = Convert.ToDecimal(row["Preco"]),
                            UnidadeDeMedida = new UnidadeDeMedida
                            {
                                Id_UnidadeDeMedida = Convert.ToInt32(row["Id"]),
                                Nome = row["nome"].ToString()

                            },
                            Categoria = new Categoria
                            {
                                Id_Categoria = Convert.ToInt32(row["id_categoria"]),
                                Nome_Categoria = row["nome"].ToString()
                            },
                            Descricao = row["descricao"].ToString()

                        };

                        lst.Add(produto);
                    }
                }
            }

            return lst;
        }
    }
}
