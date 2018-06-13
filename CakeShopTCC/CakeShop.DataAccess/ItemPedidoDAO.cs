using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ItemPedidoDAO
    {
        public void Inserir(ItemPedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO ITEM_PEDIDO (ID_PEDIDO, ID_PRODUTO, PRECO, QTD_ITEM_PRODUTO) VALUES (@ID_PEDIDO, @ID_PRODUTO, @PRECO, @QTD_ITEM_PRODUTO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = obj.Pedido.Id_Pedido;
                    cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.Int).Value = obj.Produto.Id_Produto;
                    cmd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = obj.Preco;
                    cmd.Parameters.Add("@QTD_ITEM_PRODUTO", SqlDbType.Int).Value = obj.Quantidade;

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

        public void Excluir(ItemPedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"DELETE FROM ITEM_PEDIDO WHERE ID_ITEM_PEDIDO = @ID_ITEM_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_ITEM_PEDIDO", SqlDbType.Int).Value = obj.Id;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public ItemPedido BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<ItemPedido>();
                string strSQL = @"SELECT 
                                      I.*, 
                                      P.NOME_PRODUTO 
                                  FROM ITEM_PEDIDO I 
                                  INNER JOIN PRODUTO P ON(I.ID_PRODUTO = P.ID_PRODUTO)
                                  INNER JOIN PEDIDO ON (I.ID_PEDIDO = PEDIDO.ID_PEDIDO)
                                  WHERE I.ID_ITEM_PEDIDO = @ID_ITEM_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_ITEM_PEDIDO", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var item = new ItemPedido()
                    {
                        Id = Convert.ToInt32(row["ID_ITEM_PEDIDO"]),
                        Pedido = new Pedido() { Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]) },
                        Produto = new Produto()
                        {
                            Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]),
                            Nome_Produto = row["NOME_PRODUTO"].ToString()
                        },
                        Preco = Convert.ToDecimal(row["PRECO"]),
                        Quantidade = Convert.ToInt32(row["QTD_ITEM_PRODUTO"])
                    };

                    return item;
                }
            }
        }

        public List<ItemPedido> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<ItemPedido>();
                string strSQL = @"SELECT I.* FROM ITEM_PEDIDO I;";

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
                        var item = new ItemPedido()
                        {
                            Id = Convert.ToInt32(row["ID_ITEM_PEDIDO"]),
                            Pedido = new Pedido() { Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]) },
                            Produto = new Produto() { Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]) },
                            Preco = Convert.ToDecimal(row["PRECO"]),
                            Quantidade = Convert.ToInt32(row["QTD_ITEM_PRODUTO"])
                        };

                        lst.Add(item);
                    }
                }

                return lst;
            }
        }

        public List<ItemPedido> BuscarPorPedido(int pedido)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<ItemPedido>();
                string strSQL = @"SELECT 
                                    I.*, P.NOME_PRODUTO 
                                  FROM ITEM_PEDIDO I INNER JOIN PRODUTO P ON(I.ID_PRODUTO = P.ID_PRODUTO)
                                                     INNER JOIN PEDIDO ON (I.ID_PEDIDO = PEDIDO.ID_PEDIDO)
                                                     WHERE I.ID_PEDIDO = @ID_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = pedido;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var item = new ItemPedido()
                        {
                            Id = Convert.ToInt32(row["ID_ITEM_PEDIDO"]),
                            Pedido = new Pedido() { Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]) },
                            Produto = new Produto()
                            {
                                Id_Produto = Convert.ToInt32(row["ID_PRODUTO"]),
                                Nome_Produto = row["NOME_PRODUTO"].ToString()
                            },
                            Preco = Convert.ToDecimal(row["PRECO"]),
                            Quantidade = Convert.ToInt32(row["QTD_ITEM_PRODUTO"])
                        };

                        lst.Add(item);
                    }
                }

                return lst;
            }
        }
    }
}
