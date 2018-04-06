using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class UnidadeDeMedidaDAO
    {
        public void Inserir(UnidadeDeMedida obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO UNIDADEDEMEDIDA (NOME, SIGLA) VALUES (@NOME, @SIGLA);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@SIGLA", SqlDbType.VarChar).Value = obj.Sigla;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public List<UnidadeDeMedida> BuscarTodos()
        {
            var lst = new List<UnidadeDeMedida>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM UNIDADEDEMEDIDA;";
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
                        var unidadeMedida = new UnidadeDeMedida()
                        {
                            Id_UnidadeDeMedida = Convert.ToInt32(row["ID"]),
                            Nome = row["NOME"].ToString(),
                            Sigla = row["SIGLA"].ToString()
                        };

                        lst.Add(unidadeMedida);
                    }
                }
            }

            return lst;
        }
    }
}