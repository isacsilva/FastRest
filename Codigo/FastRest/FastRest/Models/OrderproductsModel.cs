using System.ComponentModel.DataAnnotations;
using Core;

namespace FastRest.Models
{
    public class OrderproductsModel
    {
        [Display(Name = "Código:")]
        [Key]
        [Required(ErrorMessage = "Código da Order Products é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Código do Produto:")]
        [Required(ErrorMessage = "Numero do Produto do Order Products é obrigatório")]
        public int ProductId { get; set; }

        [Display(Name = "Código do Pedido:")]
        [Required(ErrorMessage = "Numero do pedido do Order Products é obrigatório")]
        public int OrderId { get; set; }

        [Display(Name = "Quantidade:")]
        [Required(ErrorMessage = "Quantidade do Order Products é obrigatório")]
        public int Quantity { get; set; }

        [Display(Name = "Preço:")]
        [Required(ErrorMessage = "Preço do Order Products é obrigatório")]
        public decimal Price { get; set; } // Corrigido para decimal
        
        public Product? Product { get; set; }
    }
}