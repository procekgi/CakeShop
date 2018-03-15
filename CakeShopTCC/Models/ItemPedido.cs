using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class ItemPedido
    {
        public int Id_pedido { get; set; }
        public string Item_produto { get; set; }
        public Produto Id_produto { get; set; }
        public int QTD_Item_produto { get; set; }

    }
}
