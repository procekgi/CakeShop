﻿using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class PedidoDAO
    {
        public void Inserir(Pedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO PEDIDO (ID_CLIENTE, DATA_PEDIDO, DATA_ENTREGA) VALUES (@ID_CLIENTE, @DATA_PEDIDO, @DATA_ENTREGA);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_CLIENTE", SqlDbType.VarChar).Value = obj.Cliente.Id_Cliente;
                    cmd.Parameters.Add("@DATA_PEDIDO", SqlDbType.DateTime).Value = obj.DataPedido;
                    cmd.Parameters.Add("@DATA_ENTREGA", SqlDbType.DateTime).Value = obj.DataEntrega.HasValue ? obj.DataEntrega.Value : new Nullable<DateTime>();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public List<Pedido> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<Pedido>();
                string strSQL = @"SELECT P.*, C.NOME_CLIENTE FROM PEDIDO P INNER JOIN CLIENTE C ON (C.ID_CLIENTE = P.ID_CLIENTE);";

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
                        var pedido = new Pedido()
                        {
                            Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]),
                            Cliente = new Cliente()
                            {
                                Id_Cliente = Convert.ToInt32(row["ID_CLIENTE"]),
                                Nome_Cliente = row["NOME_CLIENTE"].ToString()
                            },
                            DataPedido = Convert.ToDateTime(row["DATA_PEDIDO"]),
                            DataEntrega = row["DATA_ENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATA_ENTREGA"])
                        };

                        lst.Add(pedido);
                    }
                }
                return lst;
            }
        }
    }
}
