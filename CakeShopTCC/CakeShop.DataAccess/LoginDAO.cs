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
    public class LoginDAO
    {
        public void Inserir(Login obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop;
                                                            Data Source=localhost;
                                                             Integrated Security=SSPI;"))

            {
                string strSQL = @"Insert into usuario(LoginUsuario, Senha)
                                    values(@LoginUsuario, @Senha);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))

                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@LoginUsuario", SqlDbType.VarChar).Value = obj.LoginUsuario;
                        cmd.Parameters.Add("@Senha", SqlDbType.VarChar).Value = obj.Senha;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                } 
            }
        }
    }

  
}
