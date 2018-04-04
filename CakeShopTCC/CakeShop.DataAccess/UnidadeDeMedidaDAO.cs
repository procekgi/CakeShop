using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class UnidadeDeMedidaDAO
    {
        public void Inserir(UnidadeDeMedida obj)
        {
            using (SqlConnection conn = new SqlConnection
                (@"Initial Catalog=CakeShop;
                    Data Source=localhost;
                    Integrated Security=SSPI;"))
            {
                string strSQL = @"Insert into UnidadeDeMedida(Nome, Sigla)
                                        values (@Nome, @Sigla);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@Sigla", SqlDbType.VarChar).Value = obj.Sigla;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                
            }
        }

        public List<UnidadeDeMedida> BuscarTodos()
        {
            var lst = new List<UnidadeDeMedida>();
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select * from UnidadeDeMedida;";
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
                            Id_UnidadeDeMedida = Convert.ToInt32(row["Id"]),
                            Nome = row["Nome"].ToString(),
                            Sigla = row["Sigla"].ToString()
                        };

                        lst.Add(unidadeMedida);
                    }
                }
            }

            return lst;
        }
    }
}
