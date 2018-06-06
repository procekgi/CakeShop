using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ProdutoDAO
    {
        public void Inserir(Produto obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO PRODUTO (NOME_PRODUTO, PRECO, ID_UNIDADEDEMEDIDA, ID_CATEGORIA, DESCRICAO, FOTO)
                                  VALUES (@NOME_PRODUTO, @PRECO, @ID_UNIDADEDEMEDIDA, @ID_CATEGORIA, @DESCRICAO, @FOTO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME_PRODUTO", SqlDbType.VarChar).Value = obj.Nome_Produto;
                    cmd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = obj.Preco;
                    cmd.Parameters.Add("@ID_UNIDADEDEMEDIDA", SqlDbType.Int).Value = obj.UnidadeDeMedida.Id_UnidadeDeMedida;
                    cmd.Parameters.Add("@ID_CATEGORIA", SqlDbType.Int).Value = obj.Categoria.Id_Categoria;
                    cmd.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public Produto BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                    U.ID AS ID_UNIDADEDEMEDIDA,
                                    U.NOME AS NOME_UNIDADE, 
                                    C.ID AS ID_CATEGORIA, 
                                    C.NOME AS NOME_CATEGORIA, 
                                    P.ID_PRODUTO,
                                    P.NOME_PRODUTO, 
                                    P.PRECO, 
                                    P.DESCRICAO,
                                    P.FOTO 
                                FROM PRODUTO P
                                INNER JOIN UNIDADEDEMEDIDA U ON (P.ID_UNIDADEDEMEDIDA = U.ID)
                                INNER JOIN CATEGORIA C ON (P.ID_CATEGORIA = C.ID)
                                WHERE P.ID_PRODUTO = @ID_PRODUTO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var produto = new Produto()
                    {
                        Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]),
                        Nome_Produto = row["NOME_PRODUTO"].ToString(),
                        Preco = Convert.ToDecimal(row["PRECO"]),
                        UnidadeDeMedida = new UnidadeDeMedida
                        {
                            Id_UnidadeDeMedida = Convert.ToInt32(row["ID_UNIDADEDEMEDIDA"]),
                            Nome = row["NOME_UNIDADE"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            Id_Categoria = Convert.ToInt32(row["ID_CATEGORIA"]),
                            Nome_Categoria = row["NOME_CATEGORIA"].ToString()
                        },
                        Descricao = row["DESCRICAO"].ToString(),
                        Foto = row["FOTO"].ToString()
                    };

                    return produto;
                }
            }
        }

        public List<Produto> BuscarTodos()
        {
            var lst = new List<Produto>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                    U.ID AS ID_UNIDADEDEMEDIDA,
                                    U.NOME AS NOME_UNIDADE, 
                                    C.ID AS ID_CATEGORIA, 
                                    C.NOME AS NOME_CATEGORIA, 
                                    P.ID_PRODUTO,
                                    P.NOME_PRODUTO, 
                                    P.PRECO, 
                                    P.DESCRICAO,
                                    P.FOTO 
                                FROM PRODUTO P
                                INNER JOIN UNIDADEDEMEDIDA U ON (P.ID_UNIDADEDEMEDIDA = U.ID)
                                INNER JOIN CATEGORIA C ON (P.ID_CATEGORIA = C.ID);";

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
                            Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]),
                            Nome_Produto = row["NOME_PRODUTO"].ToString(),
                            Preco = Convert.ToDecimal(row["PRECO"]),
                            UnidadeDeMedida = new UnidadeDeMedida
                            {
                                Id_UnidadeDeMedida = Convert.ToInt32(row["ID_UNIDADEDEMEDIDA"]),
                                Nome = row["NOME_UNIDADE"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                Id_Categoria = Convert.ToInt32(row["ID_CATEGORIA"]),
                                Nome_Categoria = row["NOME_CATEGORIA"].ToString()
                            },
                            Descricao = row["DESCRICAO"].ToString(),
                            Foto = row["FOTO"].ToString()
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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                    U.ID AS ID_UNIDADEDEMEDIDA,
                                    U.NOME AS NOME_UNIDADE, 
                                    C.ID AS ID_CATEGORIA, 
                                    C.NOME AS NOME_CATEGORIA, 
                                    P.ID_PRODUTO,
                                    P.NOME_PRODUTO, 
                                    P.PRECO, 
                                    P.DESCRICAO, 
                                    P.FOTO
                                  FROM PRODUTO P
                                  INNER JOIN UNIDADEDEMEDIDA U ON (P.ID_UNIDADEDEMEDIDA = U.ID)
                                  INNER JOIN CATEGORIA C ON (P.ID_CATEGORIA = C.ID)
                                  WHERE P.ID_CATEGORIA = @ID_CATEGORIA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add("@ID_CATEGORIA", SqlDbType.Int).Value = categoriaId;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var produto = new Produto()
                        {
                            Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]),
                            Nome_Produto = row["NOME_PRODUTO"].ToString(),
                            Preco = Convert.ToDecimal(row["PRECO"]),
                            UnidadeDeMedida = new UnidadeDeMedida
                            {
                                Id_UnidadeDeMedida = Convert.ToInt32(row["ID_UNIDADEDEMEDIDA"]),
                                Nome = row["NOME_UNIDADE"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                Id_Categoria = Convert.ToInt32(row["ID_CATEGORIA"]),
                                Nome_Categoria = row["NOME_CATEGORIA"].ToString()
                            },
                            Descricao = row["DESCRICAO"].ToString(),
                            Foto = row["FOTO"].ToString()
                        };

                        lst.Add(produto);
                    }
                }
            }

            return lst;
        }
    }
}
