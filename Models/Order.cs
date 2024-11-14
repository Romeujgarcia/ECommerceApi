using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public int Id { get; set; }

    [Required]
    public string? UserId { get; set; } // Alterado para string

    [Required]
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Relacionamento com OrderItems

    [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser maior ou igual a 0.")]
    public decimal TotalAmount { get; set; }

    [Required]
    [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
    public DateTime OrderDate { get; set; }
}




