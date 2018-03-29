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
    }
}
