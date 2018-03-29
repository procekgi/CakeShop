using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CakeShop.DataAccess
{
    public class ClienteDAO
    {
        public void Inserir(Cliente obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"Insert into Cliente (Nome_Cliente, Telefone, Email, Login_usuario, Senha, Endereco, Numero, CEP, Cidade, Estado)
                                  values (@Nome_Cliente, @Telefone, @Email, @Login_usuario, @Senha, @Endereco, @Numero, @CEP, @Cidade, @Estado);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@Nome_Cliente", SqlDbType.VarChar).Value = obj.Nome_Cliente;
                    cmd.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = obj.Telefone;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@Login_usuario", SqlDbType.VarChar).Value = obj.Login_usuario;
                    cmd.Parameters.Add("@Senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = obj.Endereco ?? string.Empty;
                    cmd.Parameters.Add("@Numero", SqlDbType.VarChar).Value = obj.Numero ?? string.Empty;
                    cmd.Parameters.Add("@CEP", SqlDbType.VarChar).Value = obj.CEP ?? string.Empty;
                    cmd.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = obj.Cidade ?? string.Empty;
                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = obj.Estado ?? string.Empty;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public List<Cliente> BuscarTodos()
        {
            var lst = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=CakeShop; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"select * from cliente";

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
                            Id_Cliente = Convert.ToInt32(row["Id_Cliente"]),
                            Nome_Cliente = row["Nome_Cliente"].ToString(),
                            Telefone = row["Telefone"].ToString(),
                            Email = row["Email"].ToString(),
                            Login_usuario = row["Login_usuario"].ToString(),
                            Senha = row["Senha"].ToString(),
                            Endereco = row["Endereco"].ToString(),
                            Numero = row["Numero"].ToString(),
                            CEP = row["CEP"].ToString(),
                            Cidade = row["Cidade"].ToString(),
                            Estado = row["Estado"].ToString()
                        };

                        lst.Add(cliente);
                    }
                }
            }

            return lst;
        }
    }
}
