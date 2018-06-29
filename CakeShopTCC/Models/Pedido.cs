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
        public decimal ValorTotal
        {
            get
            {
                return this.Itens.Sum(item => item.Quantidade * item.Preco);
            }
        }

        public Pedido()
        {
            this.Itens = new List<ItemPedido>();
        }

        //DADOS PARA PAGAMENTO (API PAGAR.ME), NÃO SALVA NO BANCO DE DADOS
        //POR MOTIVOS DE SEGURANÇA
        public string NumeroCartao { get; set; }
        public string DataValidade { get; set; }
        public string CodigoVerificacao { get; set; }
        public string NomeDoTitular { get; set; }
    }
}