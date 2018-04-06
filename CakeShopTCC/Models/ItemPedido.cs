using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CakeShop.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
