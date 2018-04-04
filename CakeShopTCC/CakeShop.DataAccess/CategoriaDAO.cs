using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CakeShop.Models;

namespace CakeShop.DataAccess
{
    public class CategoriaDAO
    {
        public void Inserir(Categoria obj)
        {
            using (SqlConnection conn = new SqlConnection
                (@"Initial Catalog=CakeShop;
                Data Source=localhost;
                Integrated Security=SSPI;"))

            {
                string strSQL = @"insert into categoria(Nome)
                                values(@Nome);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome_Categoria;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public List<Categoria> BuscarTodos()
        {
            var lst = new List<Categoria>();
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select * from Categoria;";
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
                        var categoria = new Categoria()
                        {

                            Nome_Categoria = row["Nome"].ToString(),
                        };

                        lst.Add(categoria);
                    }
                }
            }

            return lst;
        }
    }

   

}
