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
    public class ClienteDAO
    {
        public void Inserir(Cliente obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=CakeShop;
                            Data Source=localhost;
                            Integrated Security=SSPI;"))
            {
                string strSQL = @"Insert into Cliente(Nome_Cliente, Telefone,Email,Login_usuario, Senha, Endereco, Numero, CEP, Cidade, Estado)
                                                    @Nome_Cliente,@Telefone,@Email,@Login_usuario, @Senha, @Endereco, @Numero, @CEP, @Cidade, @Estado);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@Nome_Cliente", SqlDbType.VarChar).Value = obj.Nome_Cliente;
                    cmd.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = obj.Telefone;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@Login_usuario", SqlDbType.VarChar).Value = obj.Login_usuario;
                    cmd.Parameters.Add("@Senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = obj.Endereco;
                    cmd.Parameters.Add("@Numero", SqlDbType.Int).Value = obj.Numero;
                    cmd.Parameters.Add("@CEP", SqlDbType.Int).Value = obj.CEP;
                    cmd.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = obj.Cidade;
                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = obj.Estado;


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
