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
                string strSQL = @"INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES (@NOME, @SIGLA);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@SIGLA", SqlDbType.VarChar).Value = obj.Sigla;

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

        public UnidadeDeMedida BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM UNIDADE_MEDIDA WHERE ID = @ID;";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var unidadeMedida = new UnidadeDeMedida()
                    {
                        Id_UnidadeDeMedida = Convert.ToInt32(row["ID"]),
                        Nome = row["NOME"].ToString(),
                        Sigla = row["SIGLA"].ToString()
                    };

                    return unidadeMedida;
                }
            }
        }

        public List<UnidadeDeMedida> BuscarTodos()
        {
            var lst = new List<UnidadeDeMedida>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM UNIDADE_MEDIDA;";
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