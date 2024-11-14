using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

public class CartItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; } // Relação com Product

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
    public int Quantity { get; set; }

    // Propriedades necessárias para associar ao Cart
    public int CartId { get; set; } // Id do carrinho
    public Cart Cart { get; set; }  // Relação com o carrinho
}

