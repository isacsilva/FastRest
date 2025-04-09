using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastRest.Models
{
    public class RestaurantModel
    {
        [Display(Name = "Código:")]
        [Key]
        [Required(ErrorMessage = "Código da Restaurante é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome do Produto:")]
        [Required(ErrorMessage = "Nome do Restaurante requerido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Nome da Restaurante deve ter entre 5 e 60 caracteres")]
        public required string Name { get; set; }

    }
}
