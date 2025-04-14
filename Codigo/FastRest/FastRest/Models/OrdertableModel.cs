using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastRest.Models
{
    public class OrdertableModel
    {
        [Display(Name = "Código:")]
        [Key]
        [Required(ErrorMessage = "Código da Produto é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Valor Total:")]
        [Required(ErrorMessage = "Valor Total do Produto requerido")]
        public decimal Total { get; set; }

        [Display(Name = "Status:")]
        [Required(ErrorMessage = "Nome do Produto requerido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Nome da Produto deve ter entre 5 e 60 caracteres")]
        public string Status { get; set; }

        [Display(Name = "Forma de Pagamento:")]
        [Required(ErrorMessage = "Descrição do Produto requerido")]
        public string ConsumptionMethod { get; set; }

        [Display(Name = "Código do Restaurante:")]
        [Required(ErrorMessage = "Numero restaurante da Produto é obrigatório")]
        public int RestaurantId { get; set; }
    }
}
