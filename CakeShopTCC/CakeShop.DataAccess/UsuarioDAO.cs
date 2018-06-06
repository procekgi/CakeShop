using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class UsuarioDAO
    {
        public void Inserir(Confeiteira obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO USUARIO (NOME, LOGINUSUARIO, SENHA, EMAIL) VALUES (@NOME, @LOGINUSUARIO, @SENHA, @EMAIL);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@LOGINUSUARIO", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;

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

        public List<Confeiteira> BuscarTodos()
        {
            var lst = new List<Confeiteira>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM USUARIO;";
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
                        var usuario = new Confeiteira()
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            Nome = row["NOME"].ToString(),
                            Login = row["LOGINUSUARIO"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            Email = row["EMAIL"].ToString()
                        };

                        lst.Add(usuario);
                    }
                }
            }

            return lst;
        }

        public Confeiteira Logar(Confeiteira obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM USUARIO WHERE LOGINUSUARIO = @LOGINUSUARIO AND SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@LOGINUSUARIO", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var Confeiteira = new Confeiteira()
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Nome = row["NOME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Login = row["LOGINUSUARIO"].ToString(),
                        Senha = row["SENHA"].ToString()
                    };

                    return Confeiteira;
                }
            }

        }
    }
}
