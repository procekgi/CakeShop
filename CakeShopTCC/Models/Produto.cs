


namespace CakeShop.Models
{
    public class Produto
    {
        public int Id_Produto { get; set; }
        [Required(ErrorMessage = "Campo Nome do produto é obrigatório") ]
        public string Nome_Produto { get; set; }

        [Required(ErrorMessage = "Campo Nome do produto é obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo Nome do produto é obrigatório")]
        public UnidadeDeMedida UnidadeDeMedida { get; set; }

        [Required(ErrorMessage = "Campo Nome do produto é obrigatório")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Campo Nome do produto é obrigatório")]
        public string Descricao { get; set; }

        public string Foto { get; set; }
    }
}
