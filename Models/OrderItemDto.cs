using System.ComponentModel.DataAnnotations;

public class OrderItemDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
    public int OrderId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
    public int Quantity { get; set; }
}
