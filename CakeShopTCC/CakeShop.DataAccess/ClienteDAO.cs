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
                string strSQL = @"INSERT INTO CLIENTE (NOME_CLIENTE, TELEFONE, EMAIL, LOGIN_USUARIO, SENHA, ENDERECO, NUMERO, CEP, CIDADE, ESTADO)
                                  VALUES (@NOME_CLIENTE, @TELEFONE, @EMAIL, @LOGIN_USUARIO, @SENHA, @ENDERECO, @NUMERO, @CEP, @CIDADE, @ESTADO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME_CLIENTE", SqlDbType.VarChar).Value = obj.Nome_Cliente;
                    cmd.Parameters.Add("@TELEFONE", SqlDbType.VarChar).Value = obj.Telefone;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@LOGIN_CLIENTE", SqlDbType.VarChar).Value = obj.Login_Cliente;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@ENDERECO", SqlDbType.VarChar).Value = obj.Endereco ?? string.Empty;
                    cmd.Parameters.Add("@NUMERO", SqlDbType.VarChar).Value = obj.Numero ?? string.Empty;
                    cmd.Parameters.Add("@CEP", SqlDbType.VarChar).Value = obj.CEP ?? string.Empty;
                    cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar).Value = obj.Cidade ?? string.Empty;
                    cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = obj.Estado ?? string.Empty;

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
                            Id_Cliente = Convert.ToInt32(row["ID_CLIENTE"]),
                            Nome_Cliente = row["NOME_CLIENTE"].ToString(),
                            Telefone = row["TELEFONE"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Login_Cliente = row["LOGIN_USUARIO"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            Endereco = row["ENDERECO"].ToString(),
                            Numero = row["NUMERO"].ToString(),
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
    }
}
