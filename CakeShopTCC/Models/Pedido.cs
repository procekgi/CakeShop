using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeShop.Models
{
    public class Pedido
    {
        public int Id_Pedido { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public STATUS_PEDIDO Status { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string HorarioEntrega { get; set; }
        public List<ItemPedido> Itens { get; set; }

        public Pedido()
        {
            this.Itens = new List<ItemPedido>();
        }
    }
}