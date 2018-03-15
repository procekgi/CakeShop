using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Produto
    {
        public int Id_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public decimal Preco { get; set; }
        public UnidadeDeMedida Id_UnidadeDeMedida { get; set; }
        public Categoria Id_Categoria { get; set; }
        public string Descricao { get; set; }
    }
}
