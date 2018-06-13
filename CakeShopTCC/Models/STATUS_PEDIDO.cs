using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CakeShop.Models
{
    public enum STATUS_PEDIDO
    {
        [Description("EM ANDAMENTO")]
        EM_ANDAMENTO = 1,

        [Description("PAGAMENTO REALIZADO")]
        PAGAMENTO_REALIZADO = 2,

        [Description("FINALIZADO")]
        FINALIZADO = 3
    }
}
