using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Cliente
    {
        public int Id_Cliente { get; set; } 
	    public string Nome_Cliente { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Login_usuario { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
