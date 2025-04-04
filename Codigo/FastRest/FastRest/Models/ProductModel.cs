using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastRest.Models
{
    public class ProductModel
    {
        [Display(Name = "Código:")]
        [Key]
        [Required(ErrorMessage = "Código da Farmácia é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome do Produto:")]
        [Required(ErrorMessage = "Nome do Produto requerido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Nome da farmácia deve ter entre 5 e 60 caracteres")]
        public required string Name { get; set; }

        [Display(Name = "Descrição:")]
        [Required(ErrorMessage = "Descrição do Produto requerido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Descrição da farmácia deve ter entre 5 e 60 caracteres")]
        public required string Description { get; set; }

        [Display(Name = "Preço:")]
        [Required(ErrorMessage = "Preço do Produto requerido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Descrição da farmácia deve ter entre 5 e 60 caracteres")]
        public required decimal Price { get; set; }

        [Display(Name = "Código do Restaurante:")]
        [Required(ErrorMessage = "Código da Farmácia é obrigatório")]
        public int RestaurantId { get; set; }

        [Display(Name = "Numero da Categoria:")]
        [Required(ErrorMessage = "Código da Farmácia é obrigatório")]
        public int MenuCategoryId { get; set; }

        [Display(Name = "Preço:")]
        [DataType(DataType.Date, ErrorMessage = "Código da Farmácia é obrigatório")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy")]
        public required string UpdatedAt { get; set; }
    }
}
