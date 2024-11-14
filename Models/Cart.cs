using System.ComponentModel.DataAnnotations;

public class Cart
{
    public int Id { get; set; }

    [Required]
    public string? UserId { get; set; } // Alterado para string

    [Required]
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}

