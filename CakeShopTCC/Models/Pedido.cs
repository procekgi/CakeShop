using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeShop.Models
{
    public class Pedido
    {
        public int Id_Pedido { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataEntrega { get; set; }
        public int QuantidadeDeItens { get; set; }
    }
}