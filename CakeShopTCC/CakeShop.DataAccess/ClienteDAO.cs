using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ClienteDAO
    {
        public void Inserir(Cliente obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO CLIENTE (NOME_CLIENTE, TELEFONE, EMAIL, LOGIN_CLIENTE, SENHA, ENDERECO, NUMERO, COMPLEMENTO, CEP, CIDADE, ESTADO)
                                  VALUES (@NOME_CLIENTE, @TELEFONE, @EMAIL, @LOGIN_CLIENTE, @SENHA, @ENDERECO, @NUMERO, @COMPLEMENTO, @CEP, @CIDADE, @ESTADO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME_CLIENTE", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@TELEFONE", SqlDbType.VarChar).Value = obj.Telefone;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@LOGIN_CLIENTE", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@ENDERECO", SqlDbType.VarChar).Value = obj.Endereco;
                    cmd.Parameters.Add("@NUMERO", SqlDbType.VarChar).Value = obj.Numero;
                    cmd.Parameters.Add("@COMPLEMENTO", SqlDbType.VarChar).Value = obj.Complemento;
                    cmd.Parameters.Add("@CEP", SqlDbType.VarChar).Value = obj.CEP;
                    cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar).Value = obj.Cidade;
                    cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = obj.Estado;

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

        public List<Cliente> BuscarTodos()
        {
            var lst = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM CLIENTE;";

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
                        var cliente = new Cliente()
                        {
                            Id = Convert.ToInt32(row["ID_CLIENTE"]),
                            Nome = row["NOME_CLIENTE"].ToString(),
                            Telefone = row["TELEFONE"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Login = row["LOGIN_CLIENTE"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            Endereco = row["ENDERECO"].ToString(),
                            Numero = row["NUMERO"].ToString(),
                            Complemento = row["COMPLEMENTO"].ToString(),
                            CEP = row["CEP"].ToString(),
                            Cidade = row["CIDADE"].ToString(),
                            Estado = row["ESTADO"].ToString()
                        };

                        lst.Add(cliente);
                    }
                }
            }

            return lst;
        }

        public Cliente BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("ID_CLIENTE", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var cliente = new Cliente()
                    {
                        Id = Convert.ToInt32(row["ID_CLIENTE"]),
                        Nome = row["NOME_CLIENTE"].ToString(),
                        Telefone = row["TELEFONE"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Login = row["LOGIN_CLIENTE"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        Endereco = row["ENDERECO"].ToString(),
                        Numero = row["NUMERO"].ToString(),
                        Complemento = row["COMPLEMENTO"].ToString(),
                        CEP = row["CEP"].ToString(),
                        Cidade = row["CIDADE"].ToString(),
                        Estado = row["ESTADO"].ToString()
                    };

                    return cliente;
                }
            }
        }

        public Cliente Logar(Cliente obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM CLIENTE WHERE LOGIN_CLIENTE = @LOGIN_CLIENTE AND SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@LOGIN_CLIENTE", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var cliente = new Cliente()
                    {
                        Id = Convert.ToInt32(row["ID_CLIENTE"]),
                        Nome = row["NOME_CLIENTE"].ToString(),
                        Telefone = row["TELEFONE"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Login = row["LOGIN_CLIENTE"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        Endereco = row["ENDERECO"].ToString(),
                        Numero = row["NUMERO"].ToString(),
                        Complemento = row["COMPLEMENTO"].ToString(),
                        CEP = row["CEP"].ToString(),
                        Cidade = row["CIDADE"].ToString(),
                        Estado = row["ESTADO"].ToString()
                    };

                    return cliente;
                }
            }

        }
    }
}
