using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

public class OrderItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int ProductId { get; set; }

    public Product Product { get; set; } // Relação com Product

    public int OrderId { get; set; } // Adicione esta propriedade
    public Order Order { get; set; } // Relação com Order

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
    public int Quantity { get; set; }
}

