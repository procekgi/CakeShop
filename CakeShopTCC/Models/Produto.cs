﻿

namespace CakeShop.Models
{
    public class Produto
    {
        
        public int Id_Produto { get; set; }
       
        public string Nome_Produto { get; set; }

       
        public decimal Preco { get; set; }

       public UnidadeDeMedida UnidadeDeMedida { get; set; }

       public Categoria Categoria { get; set; }

       public string Descricao { get; set; }

        public string Foto { get; set; }
    }
}
