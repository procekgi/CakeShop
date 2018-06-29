using CakeShop.Models;
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
                string strSQL = @"INSERT INTO PEDIDO ([STATUS], ID_CLIENTE, DATAPEDIDO, DATAENTREGA) VALUES (@STATUS, @ID_CLIENTE, @DATAPEDIDO, @DATAENTREGA);
                                  SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = obj.Status;
                    cmd.Parameters.Add("@ID_CLIENTE", SqlDbType.Int).Value = obj.Cliente.Id;
                    cmd.Parameters.Add("@DATAPEDIDO", SqlDbType.DateTime).Value = obj.DataPedido;
                    cmd.Parameters.Add("@DATAENTREGA", SqlDbType.DateTime).Value = obj.DataEntrega.HasValue ? obj.DataEntrega.Value : new Nullable<DateTime>();

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    obj.Id_Pedido = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
        }

        public void AtualizarDataEntrega(Pedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"UPDATE PEDIDO SET DATAENTREGA = @DATAENTREGA WHERE ID_PEDIDO = @ID_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@DATAENTREGA", SqlDbType.DateTime).Value = obj.DataEntrega;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = obj.Id_Pedido;

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

        public void AtualizarStatus(Pedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"UPDATE PEDIDO SET STATUS = @STATUS WHERE ID_PEDIDO = @ID_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = Convert.ToInt32(obj.Status);
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = obj.Id_Pedido;

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

        public void Entregar(Pedido obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"UPDATE PEDIDO SET STATUS = @STATUS WHERE ID_PEDIDO = @ID_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = STATUS_PEDIDO.FINALIZADO;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = obj.Id_Pedido;

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

        public Pedido BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                    P.*, 
                                    C.NOME_CLIENTE,
                                    C.ENDERECO,
                                    C.NUMERO,
                                    C.COMPLEMENTO,
                                    C.CEP,
                                    C.CIDADE,
                                    C.ESTADO
                                FROM PEDIDO P 
                                INNER JOIN CLIENTE C ON (C.ID_CLIENTE = P.ID_CLIENTE) 
                                WHERE ID_PEDIDO = @ID_PEDIDO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var pedido = new Pedido()
                    {
                        Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]),
                        Cliente = new Cliente()
                        {
                            Id = Convert.ToInt32(row["ID_CLIENTE"]),
                            Nome = row["NOME_CLIENTE"].ToString(),
                            Endereco = row["ENDERECO"].ToString(),
                            Numero = row["NUMERO"].ToString(),
                            Complemento = row["COMPLEMENTO"].ToString(),
                            CEP = row["CEP"].ToString(),
                            Cidade = row["CIDADE"].ToString(),
                            Estado = row["ESTADO"].ToString()
                        },
                        DataPedido = Convert.ToDateTime(row["DATAPEDIDO"]),
                        DataEntrega = row["DATAENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATAENTREGA"]),
                        Status = (STATUS_PEDIDO)Convert.ToInt32(row["STATUS"])
                    };

                    return pedido;
                }
            }
        }

        public Pedido BuscarPorCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT P.*, C.NOME_CLIENTE FROM PEDIDO P INNER JOIN CLIENTE C ON (C.ID_CLIENTE = P.ID_CLIENTE) WHERE P.ID_CLIENTE = @ID_CLIENTE;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_CLIENTE", SqlDbType.Int).Value = cliente.Id;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var pedido = new Pedido()
                    {
                        Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]),
                        Cliente = new Cliente()
                        {
                            Id = Convert.ToInt32(row["ID_CLIENTE"]),
                            Nome = row["NOME_CLIENTE"].ToString()
                        },
                        DataPedido = Convert.ToDateTime(row["DATAPEDIDO"]),
                        DataEntrega = row["DATAENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATAENTREGA"]),
                        Status = (STATUS_PEDIDO)Convert.ToInt32(row["STATUS"])
                    };

                    return pedido;
                }
            }
        }

        public Pedido Buscar(Cliente cliente, STATUS_PEDIDO status)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                    P.*, 
                                    C.NOME_CLIENTE 
                                FROM PEDIDO P 
                                INNER JOIN CLIENTE C ON (C.ID_CLIENTE = P.ID_CLIENTE) 
                                WHERE P.ID_CLIENTE = @ID_CLIENTE
                                AND P.[STATUS] = @STATUS;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_CLIENTE", SqlDbType.Int).Value = cliente.Id;
                    cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = Convert.ToInt32(status);
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var pedido = new Pedido()
                    {
                        Id_Pedido = Convert.ToInt32(row["ID_PEDIDO"]),
                        Cliente = new Cliente()
                        {
                            Id = Convert.ToInt32(row["ID_CLIENTE"]),
                            Nome = row["NOME_CLIENTE"].ToString()
                        },
                        DataPedido = Convert.ToDateTime(row["DATAPEDIDO"]),
                        DataEntrega = row["DATAENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATAENTREGA"]),
                        Status = (STATUS_PEDIDO)Convert.ToInt32(row["STATUS"])
                    };

                    return pedido;
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
                                Id = Convert.ToInt32(row["ID_CLIENTE"]),
                                Nome = row["NOME_CLIENTE"].ToString()
                            },
                            DataPedido = Convert.ToDateTime(row["DATAPEDIDO"]),
                            DataEntrega = row["DATAENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATAENTREGA"]),
                            Status = (STATUS_PEDIDO)Convert.ToInt32(row["STATUS"])
                        };

                        lst.Add(pedido);
                    }
                }
                return lst;
            }
        }

        public List<Pedido> BuscarTodosPorCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<Pedido>();
                string strSQL = @"SELECT P.*, C.NOME_CLIENTE FROM PEDIDO P INNER JOIN CLIENTE C ON (C.ID_CLIENTE = P.ID_CLIENTE) WHERE P.ID_CLIENTE = @ID_CLIENTE;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_CLIENTE", SqlDbType.Int).Value = cliente.Id;
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
                                Id = Convert.ToInt32(row["ID_CLIENTE"]),
                                Nome = row["NOME_CLIENTE"].ToString()
                            },
                            DataPedido = Convert.ToDateTime(row["DATAPEDIDO"]),
                            DataEntrega = row["DATAENTREGA"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["DATAENTREGA"]),
                            Status = (STATUS_PEDIDO)Convert.ToInt32(row["STATUS"])
                        };

                        lst.Add(pedido);
                    }
                }
                return lst;
            }
        }
    }
}
