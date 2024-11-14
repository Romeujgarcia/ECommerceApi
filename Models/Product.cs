// /Models/Product.cs
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string? Name { get; set; }

        [Range(0.01, 10000.00, ErrorMessage = "O preço deve ser entre 0.01 e 10000.00.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Url(ErrorMessage = "Informe uma URL válida.")]
        public string? ImageUrl { get; set; }
    }

}
