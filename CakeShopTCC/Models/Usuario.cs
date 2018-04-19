using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CakeShop.Models
{
    public class Usuario : ICustomPrincipal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        [ScriptIgnore]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Login, "Usuario");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Usuario()
        {

        }

        public Usuario(string login)
        {
            Identity = new GenericIdentity(login, "Usuario");
        }
    }
}
