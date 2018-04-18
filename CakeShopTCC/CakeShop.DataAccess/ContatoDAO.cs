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
    public class ContatoDAO
    {
        public void Inserir(Contato obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Contato(NomeCliente, emailCliente, Mensagem)
                                             Values(@NomeCliente, @emailCliente, @Mensagem);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@NomeCliente", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@emailCliente", SqlDbType.VarChar).Value = obj.Email;
                        cmd.Parameters.Add("@Mensagem", SqlDbType.VarChar).Value = obj.Mensagem;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
        }
    }
}
