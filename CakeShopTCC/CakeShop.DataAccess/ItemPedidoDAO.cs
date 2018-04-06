using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ItemItemPedidoDAO
    {
        public void Inserir(ItemPedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO ITEM_PEDIDO (ID_PEDIDO, ID_PRODUTO, PRECO, QTD_ITEM_PRODUTO) VALUES (@ID_PEDIDO, @ID_PRODUTO, @PRECO, @QTD_ITEM_PRODUTO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.VarChar).Value = obj.Pedido.Id_Pedido;
                    cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.VarChar).Value = obj.Produto.Id_Produto;
                    cmd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = obj.Preco;
                    cmd.Parameters.Add("@QTD_ITEM_PRODUTO", SqlDbType.Int).Value = obj.Quantidade;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
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
                            Pedido = new Pedido() { Id_Pedido = Convert.ToInt32(row["ID_CLIENTE"]) },
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
    }
}
