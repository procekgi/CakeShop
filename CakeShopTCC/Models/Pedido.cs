using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeShop.Models
{
    public class Pedido
    {
        public int Id_Pedido { get; set; }
        public Cliente Id_cliente { get; set; }
        public int QTD_Item_pedido { get; set; }
        public  string DataEntrega { get; set; }


    }
}